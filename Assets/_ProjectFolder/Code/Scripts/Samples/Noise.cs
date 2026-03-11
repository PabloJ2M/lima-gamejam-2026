using UnityEngine;
using UnityEngine.Events;

public class Noise : MonoBehaviour
{
    [SerializeField] private Vector2 _size;
    [SerializeField] private UnityEvent<float> _onValueChanged;

    private void Update() => _onValueChanged.Invoke(Mathf.PerlinNoise(_size.x, _size.y));
    private void OnDisable() => _onValueChanged.Invoke(_size.y);
}