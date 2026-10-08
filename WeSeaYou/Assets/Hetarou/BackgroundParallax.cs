using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour//AIçÏê¨ÅAñ¢åüèÿ
{
    [System.Serializable]
    public class BackgroundLayer
    {
        public GameObject background;
        public float scrollRate;

        [HideInInspector]
        public float backgroundWidth;

        [HideInInspector]
        public List<GameObject> copies = new List<GameObject>();
    }

    [SerializeField] List<BackgroundLayer> backgrounds;

    private Camera mainCamera;
    private float lastCameraX;

    private void Start()
    {
        mainCamera = Camera.main;
        lastCameraX = mainCamera.transform.position.x;

        foreach (BackgroundLayer layer in backgrounds)
        {
            if (layer.background == null)
                continue;

            SpriteRenderer spriteRenderer = layer.background.GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
                continue;

            layer.backgroundWidth = spriteRenderer.bounds.size.x;
        }
    }

    private void LateUpdate()
    {
        float cameraMoveX = mainCamera.transform.position.x - lastCameraX;

        foreach (BackgroundLayer layer in backgrounds)
        {
            if (layer.background == null || layer.backgroundWidth <= 0f)
                continue;

            MoveBackground(layer.background, cameraMoveX, layer.scrollRate);

            foreach (GameObject copy in layer.copies)
            {
                if (copy == null)
                    continue;

                MoveBackground(copy, cameraMoveX, layer.scrollRate);
            }

            CheckBackgroundLoop(layer);
        }

        lastCameraX = mainCamera.transform.position.x;
    }

    private void MoveBackground(GameObject background, float cameraMoveX, float scrollRate)
    {
        Vector3 pos = background.transform.position;
        pos.x += cameraMoveX * scrollRate;
        pos.y = mainCamera.transform.position.y;
        background.transform.position = pos;
    }

    private void CheckBackgroundLoop(BackgroundLayer layer)
    {
        float cameraLeft = mainCamera.transform.position.x
            - mainCamera.orthographicSize * mainCamera.aspect;

        float cameraRight = mainCamera.transform.position.x
            + mainCamera.orthographicSize * mainCamera.aspect;

        GameObject rightmost = layer.background;
        GameObject leftmost = layer.background;

        foreach (GameObject copy in layer.copies)
        {
            if (copy == null)
                continue;

            if (copy.transform.position.x > rightmost.transform.position.x)
                rightmost = copy;

            if (copy.transform.position.x < leftmost.transform.position.x)
                leftmost = copy;
        }

        SpriteRenderer rightSprite = rightmost.GetComponent<SpriteRenderer>();
        SpriteRenderer leftSprite = leftmost.GetComponent<SpriteRenderer>();

        if (rightSprite != null && rightSprite.bounds.max.x < cameraRight)
        {
            CreateCopy(layer, rightmost, layer.backgroundWidth);
        }

        if (leftSprite != null && leftSprite.bounds.min.x > cameraLeft)
        {
            CreateCopy(layer, leftmost, -layer.backgroundWidth);
        }
    }

    private void CreateCopy(BackgroundLayer layer, GameObject source, float offsetX)
    {
        GameObject copy = Instantiate(
            source,
            source.transform.position,
            source.transform.rotation,
            transform
        );

        Vector3 pos = copy.transform.position;
        pos.x += offsetX;
        pos.y = mainCamera.transform.position.y;
        copy.transform.position = pos;

        layer.copies.Add(copy);
    }
}
/*using System.Collections.Generic;
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
}*/