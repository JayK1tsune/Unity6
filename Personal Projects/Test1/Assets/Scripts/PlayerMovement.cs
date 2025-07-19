using Mono.Cecil;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private GameObject player;
    public InputActionReference move;
    public InputActionReference attack;
    public Vector2 _movedirection;
    void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        _movedirection = move.action.ReadValue<Vector2>();
        Vector3 movement = new Vector3(_movedirection.x, 0, _movedirection.y) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
      
    }

    public void OnEnable()
    {
        // Subscribe to the attack action
        attack.action.started += Fire;
    }
    public void OnDisable()
    {
        // Unsubscribe from the attack action
        attack.action.started -= Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Attack action triggered");
        // Implement attack logic here
    }
}

