using Unity.Cinemachine;
using UnityEngine;

public class ListenerObject : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private float _distance;

    private void Update()
    {
        if (!_camera) return;

        Vector3 cameraForward = _camera.State.GetFinalOrientation() * Vector3.forward;
        transform.position = transform.parent.position + cameraForward.normalized * _distance;
    }
}