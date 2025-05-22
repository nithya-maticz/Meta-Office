using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;        // The player to follow
    public Vector3 offset = new Vector3(0, 5, -7);  // Offset from the player
    public float smoothSpeed = 10f; // Camera follow smoothness
    public float rotationSpeed = 100f; // Mouse sensitivity

    float yaw = 0f;
    float pitch = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Get mouse input
        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -20f, 60f);

        // Calculate rotation and position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f); // Look slightly above the player
    }
}