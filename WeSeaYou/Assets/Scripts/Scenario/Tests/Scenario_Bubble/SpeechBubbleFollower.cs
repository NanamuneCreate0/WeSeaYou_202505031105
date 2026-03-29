using UnityEngine;

public class SpeechBubbleFollower : MonoBehaviour
{
    [Header("追従の設定")]
     
    [SerializeField] private RectTransform _bubbleUI;    // 動かす吹き出しのUI（自分自身でもOK）
    [SerializeField] private Vector3 _offset = new Vector3(0, 2f, 0); // キャラクターの頭上に出すためのズレ（Y軸をプラス）

    private Transform _targetCharacter; // 追従したいキャラクター（Sprite）のTransform
    private Camera _mainCamera;

    void Start()
    {
        // 座標変換には「どのカメラから見ているか」の情報が必須
        _mainCamera = Camera.main;
    }

    public void SetTarget(Transform newTarget)
    {
        _targetCharacter = newTarget;
    }

    // UpdateではなくLateUpdateを使うのがプロの鉄則
    void LateUpdate()
    {
        if (_targetCharacter == null || _bubbleUI == null) return;

        // 1. キャラクターの現在位置に、頭上用のオフセット（ズレ）を足す
        Vector3 targetWorldPos = _targetCharacter.position + _offset;

        // 2. カメラの機能を使って、ワールド座標をスクリーン（画面）のピクセル座標に変換する！
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(targetWorldPos);

        // 3. 変換した座標を、吹き出しUIの位置に代入する
        _bubbleUI.position = screenPos;
    }
}