using UnityEngine;
using UnityEngine.InputSystem;

public class S_MenuCamera : MonoBehaviour
{
    [Header("Tilt Settings")]
    public float maxAngle = 3f;        // Angle max en degrés
    public float smoothSpeed = 6f;     // Lissage

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        float mouseX = (mousePos.x / Screen.width - 0.5f) * 2f;
        float mouseY = (mousePos.y / Screen.height - 0.5f) * 2f;

        float yaw = mouseX * maxAngle;      // gauche / droite
        float pitch = -mouseY * maxAngle;     // haut / bas (inversé)

        Quaternion targetRot =
            startRotation *
            Quaternion.Euler(pitch, yaw, 0f);

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRot,
            Time.deltaTime * smoothSpeed
        );
    }
}
