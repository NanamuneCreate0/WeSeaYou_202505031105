using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundLayer
    {
        public GameObject background;
        public float scrollRate;
    }

    [SerializeField] List<BackgroundLayer> backgrounds;

    private Camera mainCamera;
    private float lastCameraX;

    private void Start()
    {
        mainCamera = Camera.main;
        lastCameraX = mainCamera.transform.position.x;
    }

    private void LateUpdate()
    {
        float cameraMoveX = mainCamera.transform.position.x - lastCameraX;

        foreach (BackgroundLayer layer in backgrounds)
        {
            if (layer.background == null)
                continue;

            Vector3 pos = layer.background.transform.position;
            pos.x += cameraMoveX * layer.scrollRate;
            layer.background.transform.position = pos;
        }

        lastCameraX = mainCamera.transform.position.x;
    }
}