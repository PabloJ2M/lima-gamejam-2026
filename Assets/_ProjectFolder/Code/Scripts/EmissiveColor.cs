using UnityEngine;

public class EmissiveColor : LightBehaviour
{
    [SerializeField] private int _index;
    [SerializeField, ColorUsage(true, true)] private Color _min, _max;
    [SerializeField] private Color _disable;

    private const string _id = "_EmissionColor";
    private Material _material;

    protected override void Awake()
    {
        base.Awake();
        _material = GetComponent<MeshRenderer>().materials[_index];
    }

    private void Update() => _material.SetColor(_id, Color.Lerp(_min, _max, Random.value));
    private void OnDisable() => TurnOn();

    public override void TurnOn() => _material.SetColor(_id, _max);
    public override void TurnOff() => _material.SetColor(_id, _disable);
}