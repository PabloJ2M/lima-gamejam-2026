using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Gameplay;

public class CheckMaskSelected : MonoBehaviour
{
    [SerializeField] private MaskSelector _selector;
    [SerializeField] private UnityEvent _onSuccess, _onMissed;

    private Paranoia _current;

    private void Awake() => SignalController.Instance.onCompleteSequence += OnCompleteSequence;
    private void OnDestroy() => SignalController.Instance.onCompleteSequence -= OnCompleteSequence;
    private void OnCompleteSequence(Paranoia paranoia) => _current = paranoia;

    public void CompareMask()
    {
        Paranoia selected = _selector.Selected ? _selector.Selected.Paranoia : Paranoia.None;
        print($"mask selected {selected}");

        if(_current == selected) {
            print("<color=green>Correct Mask Selected</color>");
            _onSuccess.Invoke();
        } else {
            print("<color=red>Missed Mask Selected</color>");
            _onMissed.Invoke();
        }
    }
}