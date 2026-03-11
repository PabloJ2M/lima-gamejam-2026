using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightObject : LightBehaviour
{
    private Light _light;

    protected override void Awake()
    {
        base.Awake();
        _light = GetComponent<Light>();
    }

    public override void TurnOn() => _light.enabled = true;
    public override void TurnOff() => _light.enabled = false;
}