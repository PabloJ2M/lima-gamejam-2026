using Unity.Cinemachine;
using UnityEngine;

public class ListenerObject : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera;
    [SerializeField] private float distance;

    private void Update()
    {
        Vector3 cameraForward = camera.State.GetFinalOrientation() * Vector3.forward;
        transform.localPosition = cameraForward.normalized * distance;
    }
}