using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChikyuSkillHand : MonoBehaviour
{
    public List<Item> FirstHandItems_Test = new List<Item>();
    public List<Item> HandItems = new List<Item>();

    public int BiginningOfHilightedRange=0;

    [SerializeField]
    List<GameObject> HandDisplayCells = new List<GameObject>();
    [SerializeField]
    GameObject HandDisplayCell;

    const float angleDistance = 36;
    const float radius=100;
    const float moveTime = 0.15f;
    const float FirstOffSetAngle = 162;

    float offSetAngle;
    float lastOffsetAngle;
    int isMoving=0;//0:静止//1:左//2:右
    float wayToMove;
    float timer;

    void Start()
    {
        //テストとして10個アイテムを持つ
        foreach(var item in FirstHandItems_Test) { HandItems.Add(item); }
        offSetAngle = FirstOffSetAngle;
        SetCellPos();
        SetItem(0);
    }

    void Update()
    {
        if (isMoving == 0 && Input.GetKeyDown(KeyCode.A))
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
        if (isMoving == 0 && Input.GetKeyDown(KeyCode.D))
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
        var rect = transform as RectTransform;

        for (int i = 0; i < HandDisplayCells.Count; i++)
        {
            RectTransform HandDisplayCell = HandDisplayCells[i].transform as RectTransform;
            float currentAngle = angleDistance * -i + offSetAngle;
            HandDisplayCell.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }

    void SetItem(int num)
    {
        if (num == 0)
        {
            for (int i = 0; i < HandDisplayCells.Count; i++)
            {
                HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[i].sprite;
            }
        }
        if (num == 1)
        {
            BiginningOfHilightedRange++;
            if (BiginningOfHilightedRange == HandItems.Count) BiginningOfHilightedRange=0;

            for (int i = 0; i < HandDisplayCells.Count ; i++)
            {
                int num1;
                num1= (i + BiginningOfHilightedRange - 1)% HandItems.Count;
                if (num1 < 0) { num1 += HandItems.Count; }
                //if (i + BiginningOfHilightedRange - 1 < 0) { num1 = i + BiginningOfHilightedRange - 1 + HandItems.Count; }
                //else if(i + BiginningOfHilightedRange - 1 >= HandItems.Count) { num1= i + BiginningOfHilightedRange - 1 - HandItems.Count; }
                //else { num1 = i + BiginningOfHilightedRange - 1; }
                HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[num1].sprite;
                /*try
                {
                    HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[num].sprite;
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Debug.Log(e.Message);
                    HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[i + BiginningOfHilightedRange - 1+HandItems.Count].sprite;
                }*/
            }
        }
        if (num == -1)
        {
            BiginningOfHilightedRange--;
            if (BiginningOfHilightedRange <0) BiginningOfHilightedRange+=HandItems.Count;
            //if (BiginningOfHilightedRange ==0) BiginningOfHilightedRange=HandItems.Count-1;
            //else if (BiginningOfHilightedRange >0) BiginningOfHilightedRange--;
            //Debug.Log(BiginningOfHilightedRange);

            for (int i = 0; i < HandDisplayCells.Count; i++)
            {
                int num1;
                num1= (i + BiginningOfHilightedRange)%HandItems.Count;
                //if (i + BiginningOfHilightedRange >= HandItems.Count) { num1 = i + BiginningOfHilightedRange - HandItems.Count; }
                //else { num1 = i + BiginningOfHilightedRange; }
                HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[num1].sprite;
            }

            /*for (int i = 0; i < HandDisplayCells.Count ; i++)
            {
                //HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[i+ BiginningOfHilightedRange].sprite;
                try
                {
                    HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[i + BiginningOfHilightedRange].sprite;
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    Debug.Log(e.Message);
                    HandDisplayCells[i].GetComponent<Image>().sprite = HandItems[i + BiginningOfHilightedRange-HandItems.Count].sprite;
                }
            }*/
        }
    }

}
