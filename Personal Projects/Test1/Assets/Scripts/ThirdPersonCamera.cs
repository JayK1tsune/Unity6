using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookat;
    public Transform camTransform;
    private Camera cam;
    public InputActionReference mouseDelta;
    public InputActionReference zoom;
    [SerializeField] private float zoomSpeed = 2.0f;
    

    [SerializeField] private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    [Range(0.1f , 10f)] [SerializeField] private float sensitivityX = 4.0f;
    [Range(0.1f , 10f)] [SerializeField] private float sensitivityY = 1.0f;

    private void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }
        camTransform = cam.transform;
    }

    private void Update()
    {
        if (zoom.action.triggered)
        {
            Vector2 zoomInput = zoom.action.ReadValue<Vector2>();
            distance -= zoomInput.y * zoomSpeed * Time.deltaTime;
            distance = Mathf.Clamp(distance, 2f, 20f); // Clamp the distance to prevent camera from getting too close or too far
        }
        Vector2 delta = mouseDelta.action.ReadValue<Vector2>();
        currentX += delta.x * sensitivityX;
        currentY -= delta.y * sensitivityY;

        currentY = Mathf.Clamp(currentY, -40f, 85f); 

        

    }
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookat.position + rotation * dir;
        camTransform.LookAt(lookat.position);
    }



}
