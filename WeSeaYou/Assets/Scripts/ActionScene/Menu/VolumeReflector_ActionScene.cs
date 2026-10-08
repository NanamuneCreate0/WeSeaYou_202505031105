using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeReflector_ActionScene : MonoBehaviour
{
    [SerializeField] private GameObject[] volumeObjects = new GameObject[11];
    private void OnEnable()
    {
        PublicStaticStatus.VolumeConfigChanged += OnVolumeConfigChanged;

        // —LŒø‰»‚³‚ê‚½Žž“_‚Ì‰¹—Ê‚à”½‰f
        OnVolumeConfigChanged();
    }

    private void OnDisable()
    {
        PublicStaticStatus.VolumeConfigChanged -= OnVolumeConfigChanged;
    }

    private void OnVolumeConfigChanged()
    {
        int volume = PublicStaticStatus.Volume_Config;

        for (int i = 0; i < volumeObjects.Length; i++)
        {
            Image image = volumeObjects[i].GetComponent<Image>();

            if (i < volume)
            {
                image.color = Color.white;
            }
            else
            {
                image.color = new Color(1f, 1f, 1f, 0.3f);
            }
        }
    }
}
