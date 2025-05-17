using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChikyuSkillHand : MonoBehaviour
{
    public List<Item> FirstHandItems_Test = new List<Item>();
    public List<Item> HandItems = new List<Item>();
    [SerializeField]
    List<GameObject> HandDisplayCells = new List<GameObject>();

    const float radius=100;
    const float moveTime = 0.2f;

    float offsetAngle = 162;
    float lastOffsetAngle;
    bool isMoving=false;
    float wayToMove;
    float timer;

    void Start()
    {
        //テストとして10個アイテムを持つ
        foreach(var item in FirstHandItems_Test) { HandItems.Add(item); }
        SetPos();
    }

    void Update()
    {
        if(!isMoving&&Input.GetKeyDown(KeyCode.A))
        {
            //offsetAngle += 36;
            //SetPos();
            lastOffsetAngle = offsetAngle;
            wayToMove = +36;
            isMoving = true;
        }

        if(isMoving)
        {
            if (timer < moveTime)
            {
                timer += Time.deltaTime;
                offsetAngle = lastOffsetAngle + (wayToMove * (timer / moveTime));
                SetPos();
            }
            else if (timer >= moveTime)
            {
                isMoving = false;
                timer = 0;
                offsetAngle =lastOffsetAngle+ wayToMove;
                SetPos();
            }
        }
    }
    void SetPos()
    {

        float splitAngle = 360 / HandDisplayCells.Count;
        var rect = transform as RectTransform;

        for (int i = 0; i < HandDisplayCells.Count; i++)
        {
            RectTransform HandDisplayCell = HandDisplayCells[i].transform as RectTransform;
            float currentAngle = splitAngle * -i + offsetAngle;
            HandDisplayCell.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;
        }
    }

}
