using UnityEngine;

namespace Unity.Cinemachine
{
    public class CinemachineFreeLookHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineInputAxisController _controller;
        [SerializeField] private CinemachineFollow _follow;
        [SerializeField] private CinemachinePanTilt _tilt;

        public void Enable() => SetState(true);
        public void Disable() => SetState(false);

        private void SetState(bool value) => _controller.enabled = _follow.enabled = _tilt.enabled = value;
    }
}