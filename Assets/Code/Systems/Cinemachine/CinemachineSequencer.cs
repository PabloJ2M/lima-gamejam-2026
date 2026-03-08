using System;
using System.Collections;
using UnityEngine;

namespace Unity.Cinemachine
{
    public class CinemachineSequencer : MonoBehaviour
    {
        [Serializable] public struct Sequence
        {
            public CinemachineCamera camera;
            public float duration;
        }
        [SerializeField] private Sequence[] _sequences;
        private int _currentIndex = 0;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            for (; _currentIndex < _sequences.Length; _currentIndex++)
            {
                SetPriority(_currentIndex);
                yield return new WaitForSeconds(_sequences[_currentIndex].duration);
            }
        }

        private void SetPriority(int index)
        {
            for (int i = 0; i < _sequences.Length; i++)
                _sequences[i].camera.Priority = index == i ? 100 : 0;
        }
        private void RefreshSequence()
        {
            StopAllCoroutines();
            StartCoroutine(Start());
        }

        public void Next()
        {
            if (_currentIndex >= _sequences.Length - 1) return;
            _currentIndex++;
            RefreshSequence();
        }
        public void Skip()
        {
            if (_currentIndex >= _sequences.Length - 1) return;
            _currentIndex = _sequences.Length - 1;
            RefreshSequence();
        }
    }
}