using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MaskSelector : MonoBehaviour
{
    [SerializeField] private InputActionReference _scroll, _press;
    [SerializeField] private Animator _anim;
    [SerializeField] private Image _indicator;
    [SerializeField] private Material _mat;
    [SerializeField] private Mask[] _masks;

    private int _index;

    public int Selected { get; private set; } = -1;
    public bool CanSelectMask { private get; set; }

    private void Awake()
    {
        _press.action.performed += PerformePress;
        _scroll.action.performed += PerformeScroll;
    }
    private void OnEnable()
    {
        _press.asset.Enable();
        _scroll.action.Enable();
    }
    private void OnDisable()
    {
        _press.action.Disable();
        _scroll.action.Disable();
    }

    private void PerformeScroll(InputAction.CallbackContext ctx)
    {
        if (_press.action.IsPressed()) return;
        if (ctx.ReadValue<Vector2>().y > 0)
            _index++;
        else if (ctx.ReadValue<Vector2>().y < 0)
            _index--;

        if (_index < 0) _index = _masks.Length - 1;
        if (_index >= _masks.Length) _index = 0;
        _indicator.sprite = _masks[_index].Sprite;
    }
    private void PerformePress(InputAction.CallbackContext ctx)
    {
        if (!CanSelectMask) return;

        bool isPressed = ctx.action.IsPressed();
        int index = isPressed ? _index : -1;

        if (isPressed) {
            if (!_masks[index].SelectMask()) return;
            _anim.SetBool("Use", true);
            _mat.SetTexture("_MainTex", _masks[index].Texture);
            SoundManager.Instance.PlaySound("PutMaskOn");
        }
        else TakeOff();

        Selected = index;
    }

    public void TakeOff()
    {
        if (Selected < 0) return;
        _masks[Selected].DeselectMask();
        _anim.SetBool("Use", false);
        SoundManager.Instance.PlaySound("TakeMaskOff");
    }
}
