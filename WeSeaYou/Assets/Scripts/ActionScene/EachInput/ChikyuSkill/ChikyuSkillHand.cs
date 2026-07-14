/*
PubulicStaticStatus.ItemListがプレイヤーが持ってる所持アイテム。
HandDisplayCellsはItemを表示する用の枠だね。回転するからこれが出てきたり消滅したりする。
そしてHandItemはそこに表示されるアイテム。
（あと、表示されるアイテムと同期して表示されるアイテムが選択可能かグレーで表示するためのリストが存在する。）

所持アイテムと表示アイテムは違うリストで、所持アイテム→表示アイテム生成。表示アイテム→所持アイテムに戻すも行われてる（表示アイテムは空のスロットを含む。）


右や左ボタンが押されるたびにアングルとハイライトがずれるというふうになってて、
これは円形UIが回転する時、アングルを使いながら必要なオブジェクト以外は壊して、上半分のみを表示している設計。
また円形UIが回転する時、ハイライトがずれて表示アイテムがその範囲表示される（アイテム数回すと一周）
 */
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChikyuSkillHand : MonoBehaviour
{
    public List<ChikyuSkillItemData> HandItems = new List<ChikyuSkillItemData>();//nullも持つ

    public int HilightStart=0;
    //private int isMoving = 0;//0:静止//1:左//2:右

    List<GameObject> HandDisplayCells = new List<GameObject>();//CellのみのGameObject（固定）
    [SerializeField]
    GameObject HandDisplayCell;
    [SerializeField]
    ChikyuSkillTable MyChikyuSkillTable;
    [SerializeField]
    Sprite NullItem;

    //長押し用
    [SerializeField]
    Image gaugeImage;
    [SerializeField]
    private BlockCreator blockCreater;
    [SerializeField]
    private ActionModeChanger actionModeChanger;

    const float angleDistance = 36;
    const float radius=210;
    const float moveTime = 0.15f;
    const float FirstOffSetAngle = 162;


    public List<bool> HandItemsBool = new List<bool>();
    float offSetAngle;
    float lastOffsetAngle;
    float wayToMove;
    float moveTimer;

    const float chargeTime = 0.15f; // 満タンまでの時間
    float currentCharge = 0f;
    bool gaugeActive=false;

    private enum Direction
    {
        Left = -1,
        None = 0,
        Right = 1
    }
    private Direction direction;



    private void OnEnable()
    {
        InputManager.Instance.actions.Player.Decide.started += OnDecideStarted;
        InputManager.Instance.actions.Player.SkillSelectRight.started += OnMoveRight;
        InputManager.Instance.actions.Player.SkillSelectLeft.started += OnMoveLeft;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.Player.Decide.started -= OnDecideStarted;
        InputManager.Instance.actions.Player.SkillSelectRight.started -= OnMoveRight;
        InputManager.Instance.actions.Player.SkillSelectLeft.started -= OnMoveLeft;
    }

    public void ActivationStart()
    {
        //HandDisplayCell関連
        for (int i = 0; i < 5; i++)
        {
            if(transform.childCount<5)
            {
                GameObject go = Instantiate(HandDisplayCell, transform);
                HandDisplayCells.Add(go);
            }
        }
        offSetAngle = FirstOffSetAngle;
        direction = Direction.None;
        SetCellPos();

        //HandItem関連
        HandItems.Clear();
        HandItemsBool.Clear();
        foreach (ChikyuSkillItemData item in PublicStaticStatus.ChikyuSkillItemList)
        {
            HandItems.Add(item);
            HandItemsBool.Add(true);
        }
        for (int i = 0; i < HandDisplayCells.Count; i++)//アイテム5個になるまでNullで埋める
        {
            if (HandItems.Count < 5) { HandItems.Add(null); HandItemsBool.Add(true); }
        }//forじゃなくてwhileでもいい
        SetItem(direction);

        //Blockを提出
        if (HandItems[0].IsBlock ==true && HandItemsBool[0])
        {
            SubmitItem(HandItems[0], 0);
            SetItem(direction);
        }
        else
        {
            Debug.LogWarning("1st Item should be Block");
        }

    }
    private void OnDecideStarted(InputAction.CallbackContext ctx)
    {
        if (direction == Direction.None)
        {
            int num = WrapIndex(HilightStart + 2, HandItems.Count);
            if (HandItems[num] != null && HandItemsBool[num])
            {
                SubmitItem(HandItems[num], num);
                SetItem(direction);
            }
            else
            {
                gaugeActive = true;
            }
        }
    }

    private void OnMoveRight(InputAction.CallbackContext ctx)
    {
        if (direction == Direction.None)
        {
            //動く用意とHandDisplayCell関連
            direction = Direction.Left;
            GameObject go = Instantiate(HandDisplayCell, transform);
            HandDisplayCells.Add(go);
            offSetAngle = FirstOffSetAngle;
            lastOffsetAngle = offSetAngle;
            wayToMove = +angleDistance;
            SetCellPos();

            SetItem(direction);
        }
    }
    private void OnMoveLeft(InputAction.CallbackContext ctx)
    {
        if (direction == Direction.None)
        {
            //動く用意
            direction = Direction.Right;
            GameObject go = Instantiate(HandDisplayCell, transform);
            HandDisplayCells.Insert(0, go);//この二つ
            offSetAngle = FirstOffSetAngle + angleDistance;//この二つが高速で処理されていい感じ
            lastOffsetAngle = offSetAngle;
            wayToMove = -angleDistance;
            SetCellPos();

            SetItem(direction);
        }
    }
    void Update()
    {
        if (InputManager.Instance.actions.Player.Decide.IsPressed() && gaugeActive)
            //学：startedやcanceledでdecidePressedを管理するのはキャッシュの思想。
            //目まぐるしく状態が変化する場合、状態の取得は、状態の真実に従う
        {
            currentCharge += Time.deltaTime;

            // ゲージ更新
            gaugeImage.fillAmount = currentCharge / chargeTime;

            // 満タン
            if (currentCharge >= chargeTime)
            {
                currentCharge = 0;
                MyChikyuSkillTable.CatchSubmitDone();
            }
        }
        else
        {
            gaugeActive = false;
            currentCharge = 0f;
            gaugeImage.fillAmount = 0f;
        }

        //「動く」ということ
        if (direction == Direction.Left)
        {
            if (moveTimer < moveTime)
            {
                moveTimer += Time.deltaTime;
                offSetAngle = lastOffsetAngle + (wayToMove * (moveTimer / moveTime));
                SetCellPos();
            }
            else if (moveTimer >= moveTime)
            {
                direction = Direction.None;
                moveTimer = 0;
                offSetAngle = lastOffsetAngle + wayToMove;
                Destroy(HandDisplayCells[0]);
                HandDisplayCells.RemoveAt(0);
                offSetAngle = FirstOffSetAngle;
                SetCellPos();
            }
        }
        if (direction == Direction.Right)
        {
            if (moveTimer < moveTime)
            {
                moveTimer += Time.deltaTime;
                offSetAngle = lastOffsetAngle + (wayToMove * (moveTimer / moveTime));
                SetCellPos();
            }
            else if (moveTimer >= moveTime)
            {
                direction = Direction.None;
                moveTimer = 0;
                offSetAngle = lastOffsetAngle + wayToMove;
                Destroy(HandDisplayCells[HandDisplayCells.Count - 1]);
                HandDisplayCells.RemoveAt(HandDisplayCells.Count - 1);
                offSetAngle = FirstOffSetAngle;
                SetCellPos();
            }
        }

    }
    void SetCellPos()//SetHandDisplayCellPosの省略
    {
        for (int i = 0; i < HandDisplayCells.Count; i++)//HandDisplayCells.Countは5か移動中は6
        {
            RectTransform HandDisplayCell = HandDisplayCells[i].transform as RectTransform;
            float currentAngle = angleDistance * -i + offSetAngle;
            HandDisplayCell.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }

    private void SetItem(Direction dir, int num = 0)
    {
        if (direction == Direction.None)
        {
            HilightStart+=num;
            WrapIndex(HilightStart, HandItems.Count);
            PaintDisplayCell(HilightStart);
        }
        else if (direction==Direction.Left)
        {
            HilightStart++;
            WrapIndex(HilightStart, HandItems.Count);
            PaintDisplayCell(HilightStart - 1);
        }
        else if (direction == Direction.Right)
        {
            HilightStart--;
            WrapIndex(HilightStart, HandItems.Count);
            PaintDisplayCell(HilightStart);
        }
    }
    void PaintDisplayCell(int HandItemsOffset)//HandDisplayCells_HandItems_RoughDifference//DisplayCellは基本左から右で0～
    {
        for (int i = 0; i < HandDisplayCells.Count; i++)
        {
            int itemIndex = WrapIndex(i + HandItemsOffset, HandItems.Count);

            Image img = HandDisplayCells[i].GetComponent<Image>();
            ChikyuSkillItemData item = HandItems[itemIndex];
            bool selectable = HandItemsBool[itemIndex];
            img.sprite = item != null ? item.sprite : NullItem;
            img.color = selectable ? Color.white : Color.gray;
        }
    }

    void SubmitItem(ChikyuSkillItemData item,int num)
    {
        HandItemsBool[num] = false;
        MyChikyuSkillTable.CatchSubmitItem(item);
    }

    private int WrapIndex(int index, int count)
    {
        if (count <= 0) return 0; // 空リスト回避
        int result = index % count;
        if (result < 0) result += count;
        return result;
    }
}
