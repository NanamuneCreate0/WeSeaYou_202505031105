using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] bool isGoal=false;
    public void Activate()
    {
        if (isGoal)
        {
            PublicStaticStatus.ClearedChapter ++;
            Debug.Log(PublicStaticStatus.ClearedChapter +"‚É‚«‚Ü‚µ‚½");
        }
        SceneManager.LoadScene("StillScene01");
    }
}
