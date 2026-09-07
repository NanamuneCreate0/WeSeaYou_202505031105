using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class OperableAbility : MonoBehaviour
{
    void Start()
    {
        gameObject.AddComponent<VelocityProviderObj>();
        gameObject.AddComponent<OperableObj>();
    }
}
