using System;
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    [SerializeField] private float chargeTime = 2f;
    private Rigidbody rb;
    private GameObject player;
    private LayerMask groundLayer;
    public InputActionReference move;
    public InputActionReference attack;
    public InputActionReference jump;
    public Vector2 _movedirection;
    ColourChange colourChange;
    public Slider chargeSlider;



    void Start()
    {
        chargeSlider.value = 0;
        colourChange = FindAnyObjectByType<ColourChange>();
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        _movedirection = move.action.ReadValue<Vector2>();
        Vector3 movement = new Vector3(_movedirection.x, 0, _movedirection.y) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void OnEnable()
    {
        jump.action.performed += _ => Jump();
        attack.action.started += OnAttackStarted;

    }
    public void OnDisable()
    {
        attack.action.started -= Fire;
        jump.action.started -= _ => Jump();
        attack.action.performed -= OnAttackStarted;

    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Attack action triggered");
        // Implement attack logic here
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        StartCoroutine(ColourChangeHold(chargeTime));
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }


    IEnumerator ColourChangeHold(float holdTime)
    {
        float elapsed = 0f;
        Color startColor = getPreviousColour();
        Color targetColor = GetCurrentColour();
        while (elapsed < holdTime)
        {
            if (attack.action.IsPressed())
            {
                elapsed += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(elapsed / holdTime);
                Color lastColor = startColor;
                Color currentColor = GetCurrentColour();
                Debug.Log($"Lerping from {lastColor} to {currentColor}");
                chargeSlider.fillRect.GetComponent<Image>().color = Color.Lerp(startColor, targetColor, fillAmount);
                chargeSlider.value = fillAmount; // Update the slider value
            }
            else
            {
                // Button was released too early
                Debug.Log("Hold time not reached, colour change cancelled");
                chargeSlider.value = 0; // Reset the slider
                chargeSlider.fillRect.GetComponent<Image>().color = startColor;
                yield break; // Exit coroutine early
            }

            yield return null; // Wait for next frame
        }

        Debug.Log("Hold time reached, changing color");
        colourChange.ChangeColour();
        chargeSlider.value = 0;
    }


    private Color GetCurrentColour()
    {
        return colourChange.mat.color;
    }

    private Color getPreviousColour()
    {
        if (colourChange.colors.Count >= 2)
        {
            return colourChange.colors[colourChange.colors.Count - 2]; // Previous color
        }
        else
        {
            return Color.white; // Fallback
        }
    }

    

}

