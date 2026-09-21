using UnityEngine;

public class MenuFunctionSample : MonoBehaviour, IMenuFunction
{
    public void Execute()
    {
        Debug.Log("MenuFunctionSample Execute");
    }
}