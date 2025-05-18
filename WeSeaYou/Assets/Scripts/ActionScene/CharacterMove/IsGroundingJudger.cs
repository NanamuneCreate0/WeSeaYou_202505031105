using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundingJudger : MonoBehaviour
{
    public bool IsGrounding;
    void Start()
    {
        IsGrounding = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounding = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsGrounding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounding = false;
    }
}
