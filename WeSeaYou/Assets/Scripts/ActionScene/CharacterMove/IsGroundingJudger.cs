using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundingJudger : MonoBehaviour
{
    public bool IsGrounding;
    public Collider2D CurrentGroundCollider;
    void Start()
    {
        IsGrounding = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounding = true;
        CurrentGroundCollider = collision;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsGrounding = true;
        CurrentGroundCollider = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounding = false;
    }
}
