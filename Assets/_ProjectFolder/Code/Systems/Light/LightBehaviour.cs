using UnityEngine;

public abstract class LightBehaviour : MonoBehaviour, ILight
{
    protected virtual void Awake() => LightController.Instance.AddListener(this);
    protected virtual void OnDestroy() => LightController.Instance.RemoveListener(this);

    public abstract void TurnOn();
    public abstract void TurnOff();
}