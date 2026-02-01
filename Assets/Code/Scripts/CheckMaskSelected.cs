using UnityEngine;
using UnityEngine.Events;

public class CheckMaskSelected : MonoBehaviour
{
    [SerializeField] private SignalManager _signals;
    [SerializeField] private MaskSelector _selector;

    [SerializeField] private UnityEvent _onSuccess, _onMissed;

    public void CompareMask()
    {
        Paranoia paranoia = (_selector.Selected) switch { 0 => Paranoia.Exito, 1 => Paranoia.Observado, 2 => Paranoia.Tecnologia, _ => Paranoia.None };
        
        if (_signals.Paranoia == paranoia)
            _onSuccess.Invoke();
        else
            _onMissed.Invoke();
    }
}