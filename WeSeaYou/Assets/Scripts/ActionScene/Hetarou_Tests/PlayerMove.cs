using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Vector2として読み込むが...
        Vector2 input = InputManager.Instance.actions.Player.Move.ReadValue<Vector2>();

        // 2. Yの値は使わず、Xの値だけを使って移動ベクトルを作る
        // もし手順1で「1D Axis」にしたなら、float input = ...ReadValue<float>(); になります
        Vector3 moveDirection = new Vector3(input.x, 0, 0);

        // 3. 移動
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
