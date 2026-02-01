using UnityEngine;

public class MaterialColor : MonoBehaviour
{
    [SerializeField] private Color _color;

    private const string _id = "_BaseColor";
    private Material _material;
    private Color _baseColor;

    private void Awake() => _material = GetComponent<MeshRenderer>().material;
    private void Start() => _baseColor = _material.color;

    public void DisplayColor()
    {
        _material.SetColor(_id, _color);
        Invoke(nameof(ResetColor), 1f);
    }
    private void ResetColor()
    {
        _material.SetColor(_id, _baseColor);
    }
}