using UnityEngine;

public class TextAAA : MonoBehaviour
{
    [SerializeField]
    GameObject test;
    void Start()
    {
        Instantiate(test, transform);
        Instantiate(test, transform);
        Instantiate(test, transform);
        Instantiate(test, transform);
        Instantiate(test, transform);
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(1).gameObject);
        Debug.Log(transform.childCount+"test");
    }

}
