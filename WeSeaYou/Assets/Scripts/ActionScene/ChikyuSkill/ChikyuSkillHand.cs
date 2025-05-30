using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChikyuSkillHand : MonoBehaviour
{
    //public List<Item> FirstHandItems_Test = new List<Item>();
    public List<Item> HandItems = new List<Item>();

    public int HilightStart=0;
    public int isMoving = 0;//0:静止//1:左//2:右

    [SerializeField]
    List<GameObject> HandDisplayCells = new List<GameObject>();//CellのみのGameObject（固定）
    [SerializeField]
    GameObject HandDisplayCell;
    [SerializeField]
    ChikyuSkillTable MyChikyuSkillTable;
    [SerializeField]
    ChikyuSkillCursor MyChikyuSkillCursor;

    const float angleDistance = 36;
    const float radius=100;
    const float moveTime = 0.15f;
    const float FirstOffSetAngle = 162;

    float offSetAngle;
    float lastOffsetAngle;
    float wayToMove;
    float timer;

    void Start()
    {
        //foreach(var item in FirstHandItems_Test) { HandItems.Add(item); }//テストとして10個アイテムを持つ
        HandItems.Clear();
        foreach(Item item in PublicStaticStatus.ItemList) { HandItems.Add(item); }
        for (int i = 0; i < HandDisplayCells.Count; i++) { if (HandItems.Count < 5) { HandItems.Add(null); } }//forじゃなくてwhileでもいい
        offSetAngle = FirstOffSetAngle;
        SetCellPos();
        SetItem(0);
    }

    void Update()
    {
        //ボタン押せるのはCursorModeが「Hand動かす」ときだけ
        if (MyChikyuSkillCursor.CursorMode == 0)
        {
            //決定
            if (isMoving == 0 && Input.GetKeyDown(KeyCode.C))
            {
                int num = (HilightStart + 2) % HandItems.Count;
                if (HandItems[num] != null)
                {
                    Debug.Log(HandItems[num].itemName + " Chosen");
                    SubmitItem(HandItems[num]);

                }
                else if (HandItems[num] == null)
                {
                    Debug.Log("null Chosen");
                }
            }
            //右に動かす
            if (isMoving == 0 && Input.GetKeyDown(KeyCode.D))
            {
                GameObject go = Instantiate(HandDisplayCell, transform);
                HandDisplayCells.Add(go);
                offSetAngle = FirstOffSetAngle;
                SetCellPos();
                SetItem(+1);

                //動く用意
                lastOffsetAngle = offSetAngle;
                wayToMove = +angleDistance;
                isMoving = 1;
            }
            //左に動かす
            else if (isMoving == 0 && Input.GetKeyDown(KeyCode.A))
            {
                GameObject go = Instantiate(HandDisplayCell, transform);
                HandDisplayCells.Insert(0, go);//この二つ
                offSetAngle = FirstOffSetAngle + angleDistance;//この二つが高速で処理されていい感じ
                SetCellPos();
                SetItem(-1);

                //動く用意
                lastOffsetAngle = offSetAngle;
                wayToMove = -angleDistance;
                isMoving = 2;
            }
            
        }

        //「動く」ということ
        if (isMoving == 1)
        {
            if (timer < moveTime)
            {
                timer += Time.deltaTime;
                offSetAngle = lastOffsetAngle + (wayToMove * (timer / moveTime));
                SetCellPos();
            }
            else if (timer >= moveTime)
            {
                isMoving = 0;
                timer = 0;
                offSetAngle = lastOffsetAngle + wayToMove;
                Destroy(HandDisplayCells[0]);
                HandDisplayCells.RemoveAt(0);
                offSetAngle = FirstOffSetAngle;
                SetCellPos();
            }
        }
        if (isMoving == 2)
        {
            if (timer < moveTime)
            {
                timer += Time.deltaTime;
                offSetAngle = lastOffsetAngle + (wayToMove * (timer / moveTime));
                SetCellPos();
            }
            else if (timer >= moveTime)
            {
                isMoving = 0;
                timer = 0;
                offSetAngle = lastOffsetAngle + wayToMove;
                Destroy(HandDisplayCells[HandDisplayCells.Count - 1]);
                HandDisplayCells.RemoveAt(HandDisplayCells.Count-1);
                offSetAngle = FirstOffSetAngle;
                SetCellPos();
            }
        }

    }
    void SetCellPos()//SetHandDisplayCellPosの省略
    {
        //Debug.Log(HandDisplayCells.Count);//これがセルがいくつ表示されるかを決定している//これもconstにしたい
        for (int i = 0; i < HandDisplayCells.Count; i++)
        {
            RectTransform HandDisplayCell = HandDisplayCells[i].transform as RectTransform;
            float currentAngle = angleDistance * -i + offSetAngle;
            HandDisplayCell.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }

    void SetItem(int num0)
    {
        if (num0 == 0)
        {
            for (int i = 0; i < HandDisplayCells.Count; i++)
            {
                if(HandItems[i] != null)
                {
                    HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[i].sprite;
                }
                else if(HandItems[i] != null)
                {
                    //HandDisplayCells[i].GetComponent<Image>().sprite =//透明にするとか
                }
            }
        }
        if (num0 == 1)
        {
            HilightStart++;
            if (HilightStart == HandItems.Count) HilightStart=0;

            for (int i = 0; i < HandDisplayCells.Count ; i++)
            {
                int num1= (i + HilightStart - 1)% HandItems.Count;
                if (num1 < 0) { num1 += HandItems.Count; }
                if (HandItems[num1] != null)
                {
                    HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[num1].sprite;
                }
                else if (HandItems[num1] != null)
                {
                    //HandDisplayCells[i].GetComponent<Image>().sprite =//透明にするとか
                }
            }
        }
        if (num0 == -1)
        {
            HilightStart--;
            if (HilightStart <0) HilightStart+=HandItems.Count;
            for (int i = 0; i < HandDisplayCells.Count; i++)
            {
                int num1= (i + HilightStart)%HandItems.Count;
                if (num1 < 0) { num1 += HandItems.Count; }
                if (HandItems[num1] != null)
                {
                    HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[num1].sprite;
                }
                else if (HandItems[num1] != null)
                {
                    //HandDisplayCells[i].GetComponent<Image>().sprite =//透明にするとか
                }
            }
        }
    }
    
    void SubmitItem(Item item)
    {
        MyChikyuSkillTable.GetItem(item);
    }
}
