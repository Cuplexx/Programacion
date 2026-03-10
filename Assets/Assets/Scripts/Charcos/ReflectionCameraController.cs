using UnityEngine;

public class ReflectionCameraController : MonoBehaviour
{
    public Transform playerCamera;

    void LateUpdate()
    {
        Vector3 rot = playerCamera.eulerAngles;

        float X = playerCamera.eulerAngles.x;
        float Z = playerCamera.eulerAngles.z;

        transform.rotation = Quaternion.Euler(-X, rot.y - 180f, Z);
    }
}