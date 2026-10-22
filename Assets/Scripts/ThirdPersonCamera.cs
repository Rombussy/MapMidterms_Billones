using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offsets & Distance")]
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float height = 2.0f;
    [SerializeField] private float smoothSpeed = 10.0f;

    [Header("Sensitivity & Limits")]
    [SerializeField] private float mouseSensitivity = 3.0f;
    [SerializeField] private float minPitch = -20.0f;
    [SerializeField] private float maxPitch = 60.0f;

    private float yaw;
    private float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);

        transform.rotation = rotation;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}