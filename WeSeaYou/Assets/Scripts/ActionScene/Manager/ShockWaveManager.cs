using System.Collections;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] private float _shockWaveDuration = 0.75f; // ショック
    private Coroutine _shockWaveCoroutine;
    private Material _material; 
    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");
    void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material; // SpriteRendererのマテリアルを取得
    }

    public void StartShockWave()
    {
        _shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f)); // ショックウェーブの開始位置と終了位置を指定してコルーチンを開始
    }

    private IEnumerator ShockWaveAction(float startPos, float endPos)
    {
        _material.SetFloat(_waveDistanceFromCenter, startPos); // ショックウェーブの開始位置をシェーダーに設定
        
        float lerpedAmout = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _shockWaveDuration)
        {
            elapsedTime += Time.deltaTime;
            lerpedAmout = Mathf.Lerp(startPos, endPos, elapsedTime / _shockWaveDuration); // ショックウェーブの位置を線形補間で更新
            _material.SetFloat(_waveDistanceFromCenter, lerpedAmout); // シェーダーに更新された位置を設定
            yield return null; // 次のフレームまで待機
        }
    }
}
