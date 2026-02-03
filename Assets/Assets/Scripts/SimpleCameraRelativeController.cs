using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCameraRelativeController : MonoBehaviour
{
    public Transform target;

    public float mouseSensitivity = 3f;
    public float minY = -40f;
    public float maxY = 70f;
    public float distance = 4f;

    private float yaw;
    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        transform.position = target.position;
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
