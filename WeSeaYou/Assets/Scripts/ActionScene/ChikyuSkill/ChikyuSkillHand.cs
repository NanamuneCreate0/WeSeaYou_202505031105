using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class ChikyuSkillHand : MonoBehaviour
{
    public List<Item> HandItems = new List<Item>();//null������

    public int HilightStart=0;
    public int isMoving = 0;//0:�Î~//1:��//2:�E

    [SerializeField]
    List<GameObject> HandDisplayCells = new List<GameObject>();//Cell�݂̂�GameObject�i�Œ�j
    [SerializeField]
    GameObject HandDisplayCell;
    [SerializeField]
    ChikyuSkillTable MyChikyuSkillTable;
    [SerializeField]
    ChikyuSkillCursor MyChikyuSkillCursor;
    [SerializeField]
    Sprite NullItem;

    const float angleDistance = 36;
    const float radius=210;
    const float moveTime = 0.15f;
    const float FirstOffSetAngle = 162;


    public List<bool> HandItemsBool = new List<bool>();
    float offSetAngle;
    float lastOffsetAngle;
    float wayToMove;
    float timer;

    public void ActivationStart()
    {
        HandItems.Clear();
        HandItemsBool.Clear();
        foreach (Item item in PublicStaticStatus.ItemList)
        {
            HandItems.Add(item);
            HandItemsBool.Add(true);
        }
        for (int i = 0; i < HandDisplayCells.Count; i++) { if (HandItems.Count < 5) { HandItems.Add(null); HandItemsBool.Add(true); } }//for����Ȃ���while�ł�����
        SetItem(0);

        offSetAngle = FirstOffSetAngle;
        SetCellPos();
    }


    void Update()
    {
        //�{�^��������̂�Mixing�������s���Ă��Ȃ��Ƃ�
        if (!MyChikyuSkillTable.IsMixing)
        {
            //����
            if (isMoving == 0 && Input.GetKeyDown(KeyCode.C))
            {
                int num = (HilightStart + 2) % HandItems.Count;
                if (num < 0) { num += HandItems.Count; }
                if (HandItems[num] != null && HandItemsBool[num])
                {
                    Debug.Log(HandItems[num].itemName + " Chosen");
                    HandItemsBool[num] = false;
                    SubmitItem(HandItems[num],num);
                    SetItem(0);
                }
                else
                {
                    Debug.Log("null Chosen");
                }
            }
            //�E�ɓ�����
            if (isMoving == 0 && Input.GetKeyDown(KeyCode.D))
            {
                GameObject go = Instantiate(HandDisplayCell, transform);
                HandDisplayCells.Add(go);
                offSetAngle = FirstOffSetAngle;
                SetCellPos();
                SetItem(+1);

                //�����p��
                lastOffsetAngle = offSetAngle;
                wayToMove = +angleDistance;
                isMoving = 1;
            }
            //���ɓ�����
            else if (isMoving == 0 && Input.GetKeyDown(KeyCode.A))
            {
                GameObject go = Instantiate(HandDisplayCell, transform);
                HandDisplayCells.Insert(0, go);//���̓��
                offSetAngle = FirstOffSetAngle + angleDistance;//���̓�������ŏ�������Ă�������
                SetCellPos();
                SetItem(-1);

                //�����p��
                lastOffsetAngle = offSetAngle;
                wayToMove = -angleDistance;
                isMoving = 2;
            }
            
        }

        //�u�����v�Ƃ�������
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
    void SetCellPos()//SetHandDisplayCellPos�̏ȗ�
    {
        //Debug.Log(HandDisplayCells.Count);//���ꂪ�Z���������\������邩�����肵�Ă���//�����const�ɂ�����
        for (int i = 0; i < HandDisplayCells.Count; i++)
        {
            RectTransform HandDisplayCell = HandDisplayCells[i].transform as RectTransform;
            float currentAngle = angleDistance * -i + offSetAngle;
            HandDisplayCell.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }

    public void SetItem(int num0)
    {
        if (num0 == 0)
        {
            PaintDisplayCell(HilightStart); ;
        }
        if (num0 == 1)
        {
            HilightStart++;
            if (HilightStart == HandItems.Count) HilightStart = 0;

            PaintDisplayCell(HilightStart - 1);
        }
        if (num0 == -1)
        {
            HilightStart--;
            if (HilightStart < 0) HilightStart += HandItems.Count;

            PaintDisplayCell(HilightStart);
        }
    }
    void PaintDisplayCell(int RoughDifference_BetweenHandDisplayCellsAndHandItems)//���̐���傫�Ȑ��ł��Ή��ł���//�����Rough//DisplayCell�͊�{������E��0�`
    {
        for (int i = 0; i < HandDisplayCells.Count; i++)
        {
            int num1 = (i + RoughDifference_BetweenHandDisplayCellsAndHandItems) % HandItems.Count;
            if (num1 < 0) { num1 += HandItems.Count; }
            Image img = HandDisplayCells[i].GetComponent<Image>();
            if (HandItems[num1] != null)
            {
                img.sprite = HandItems[num1].sprite;
                if (!HandItemsBool[num1]) { img.color = Color.gray; }
                else { img.color = Color.white; }
            }
            else if (HandItems[num1] == null)
            {
                img.sprite = NullItem;
                if (!HandItemsBool[num1]) { img.color = Color.gray; }
                else { img.color = Color.white; }
            }
        }
    }
    
    void SubmitItem(Item item,int num)
    {
        MyChikyuSkillTable.ChatchSubmitItem(item,num);
    }

    public void ConfirmStaticItemList(bool ExcuteSort)
    {
        PublicStaticStatus.ItemList.Clear();
        for (int i = 0; i < HandItems.Count; i++)
        {
            if (HandItems[i] != null)
            {
                PublicStaticStatus.ItemList.Add(HandItems[i]);
            }
        }
        if (ExcuteSort) { PublicStaticStatus.ItemList.Sort((a, b) => a.ID.CompareTo(b.ID)); }
    }

    public void RefreshHandItemsBool()
    {
        HandItemsBool.Clear();
        for (int i = 0; i < HandItems.Count; i++) { HandItemsBool.Add(true); }
    }
}
