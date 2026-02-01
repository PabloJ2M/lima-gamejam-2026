using UnityEngine;
using UnityEngine.InputSystem;

public class MaskSelector : MonoBehaviour
{
    [SerializeField] private InputActionReference _reference;
    [SerializeField] private Mask[] _masks;

    public int Selected { get; private set; } = -1;
    public bool CanSelectMask { private get; set; }

    private void Awake() => _reference.action.performed += Performe;
    private void OnEnable() => _reference.action.Enable();
    private void OnDisable() => _reference.action.Disable();

    private void Performe(InputAction.CallbackContext ctx)
    {
        if (!CanSelectMask) return;

        bool isPressed = ctx.action.IsPressed();
        int index = isPressed ? (int)ctx.ReadValue<float>() : -1;

        if (isPressed) {
            if (!_masks[index].SelectMask()) return;
        }
        else TakeOff();

        Selected = index;
    }

    public void TakeOff()
    {
        if (Selected >= 0)
            _masks[Selected].DeselectMask();
    }
}
