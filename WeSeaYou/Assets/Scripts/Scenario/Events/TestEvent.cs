using System.Collections;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private Transform Sea;
    [SerializeField] private Transform Mare;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TestScenarioEventColutine());
        }
    }

    private IEnumerator TestScenarioEventColutine()
    {
        float duration = 2f;
        Vector2 seaPos = Sea.position;
        Vector2 marePos = Mare.position;

        // Sea ‚ð2•b“®‚©‚·
        float time = 0f;
        while (time < duration)
        {
            seaPos.x += 2f * Time.deltaTime;
            Sea.position = seaPos;
            time += Time.deltaTime;
            yield return null;
        }

        // time ‚ðƒŠƒZƒbƒg‚µ‚Ä‚©‚ç Mare ‚ð2•b“®‚©‚·
        time = 0f;
        while (time < duration)
        {
            marePos.x += 2f * Time.deltaTime;
            Mare.position = marePos;
            time += Time.deltaTime;
            yield return null;
        }
    }
}
