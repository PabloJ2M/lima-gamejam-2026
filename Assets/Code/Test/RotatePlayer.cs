using UnityEngine;
using UnityEngine.Windows;

public class RotatePlayer : MonoBehaviour
{
    public Transform cameraTransform;

    public float rotationSpeed = 6f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Dirección de la cámara (PLANA)
        Vector3 camForward = cameraTransform.forward;

        camForward.y = 0f;

        camForward.Normalize();

        if (camForward.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        /*Vector3 moveDir = camForward + camRight;

        // Movimiento
        //controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Rotación suave hacia donde se mueve
        Quaternion targetRot = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );*/
    }
}
