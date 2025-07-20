using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColourChange : MonoBehaviour
{

    [SerializeField] GameObject targetObject;
    public InputActionReference changeColourAction;
    private Material mat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = targetObject.GetComponent<Renderer>().material;

    }

    public void OnEnable()
    {
        changeColourAction.action.performed += ChangeColour;
    }
    public void OnDisable()
    {
        changeColourAction.action.performed -= ChangeColour;
    }

    private void ChangeColour(InputAction.CallbackContext context)
    {
        Color newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        mat.color = newColor;
    }

}
