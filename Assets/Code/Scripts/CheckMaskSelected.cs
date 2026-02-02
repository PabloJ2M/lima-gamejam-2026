using UnityEngine;
using UnityEngine.Events;

public class CheckMaskSelected : MonoBehaviour
{
    [SerializeField] private SignalManager _signals;
    [SerializeField] private MaskSelector _selector;

    [SerializeField] private UnityEvent _onSuccess, _onMissed;

    public void CompareMask()
    {
        Paranoia paranoia = (_selector.Selected) switch {
            0 => Paranoia.Observado, 1 => Paranoia.Tecnologia, 2 => Paranoia.Exito, _ => Paranoia.None
        };

        if(_signals.Paranoia == paranoia) {
            print("<color=green>Correct Mask Selected</color>");
            _onSuccess.Invoke();
        } else {
            print("<color=red>Missed Mask Selected</color>");
            _onMissed.Invoke();
        }
    }
}