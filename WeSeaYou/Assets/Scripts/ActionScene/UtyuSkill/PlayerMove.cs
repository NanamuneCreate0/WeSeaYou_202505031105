using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    void Update()
    {
        Vector2 input = InputManager.Instance.actions.Player.Move.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(input.x, 0, 0);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
