using UnityEngine;

public class StaticFPSCamera : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;

    [Header("Vertical Limits")]
    [Tooltip("Mínimo ángulo hacia abajo")]
    public float minPitch = -80f;

    [Tooltip("Máximo ángulo hacia arriba")]
    public float maxPitch = 80f;

    float pitch = 0f;

    void Start()
    {
        // Bloquea el cursor (opcional)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación vertical (arriba / abajo)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, transform.localEulerAngles.y, 0f);

        // Rotación horizontal (izquierda / derecha)
        transform.Rotate(Vector3.up * mouseX, Space.World);
    }
}
