using UnityEngine;
using UnityEngine.Events;

public class CheckMaskSelected : MonoBehaviour
{
    [SerializeField] private SignalManager _signals;
    [SerializeField] private MaskSelector _selector;

    [SerializeField] private UnityEvent _onSuccess, _onMissed;

    public void CompareMask()
    {
        Paranoia selected = _selector.Selected ? _selector.Selected.Paranoia : Paranoia.None;
        print($"mask selected {selected}");

        if(_signals.Paranoia == selected) {
            print("<color=green>Correct Mask Selected</color>");
            _onSuccess.Invoke();
        } else {
            print("<color=red>Missed Mask Selected</color>");
            _onMissed.Invoke();
        }
    }
}