using System.Collections.Generic;
using UnityEngine;

public class LightController : SingletonBasic<LightController>
{
    [SerializeField] private Light[] _lightSequence;

    private List<ILight> _secondaryLights = new();
    
    public void AddListener(ILight light) => _secondaryLights.Add(light);
    public void RemoveListener(ILight light) => _secondaryLights.Remove(light);

    public void SetLightIndex(int index)
    {
        for (int i = 0; i < _lightSequence.Length; i++)
            _lightSequence[i].enabled = i == index;
    }

    public void TurnOnLights()
    {
        foreach (var light in _secondaryLights)
            light.TurnOn();
    }
    public void TurnOffLights()
    {
        foreach (var light in _secondaryLights)
            light.TurnOff();
    }
}