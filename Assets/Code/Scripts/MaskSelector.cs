using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Interaction;

public class MaskSelector : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Timeout _timer;

    [SerializeField] private Animator _maskGroup;
    [SerializeField] private Animator _character;
    [SerializeField] private Transform _targetIK;
    [SerializeField] private Material _mat;

    [Header("Sounds")]
    [SerializeField] private EventReference putMaskOn;
    [SerializeField] private EventReference takeMaskOff;

    private int _index;
    public SelectableMask Selected { get; private set; }

    private void OnEnable()
    {
        _maskGroup.ResetControllerState();
        _maskGroup.SetBool("IsDisplayed", true);
    }

    public void Preview(Sprite sprite)
    {
        if (sprite)
            _preview.sprite = sprite;
    }
    public void Interact(SelectableMask mask)
    {
        Selected = mask;
        SoundManager.Instance.PlaySound(putMaskOn);

        _targetIK.position = mask.transform.position;

        _character.SetBool("IsOn", true);
        _maskGroup.SetBool("IsDisplayed", false);
        _mat.SetTexture("_MainTex", mask.Texture);
        _timer.CompleteTimeout();
    }

    public void TakeOff()
    {
        if (!Selected) return;

        Selected = null;
        _character.SetBool("IsOn", false);
        SoundManager.Instance.PlaySound(takeMaskOff);
    }
}