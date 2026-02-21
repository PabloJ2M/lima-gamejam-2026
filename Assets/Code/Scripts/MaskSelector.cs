using UnityEngine;
using UnityEngine.UI;
using Gameplay.Interaction;

public class MaskSelector : MonoBehaviour
{
    [SerializeField] private Image _preview;
    [SerializeField] private Timeout _timer;

    [SerializeField] private Animator _character;
    [SerializeField] private Material _mat;

    public SelectableMask Selected { get; private set; }

    public void Preview(Sprite sprite)
    {
        if (sprite)
            _preview.sprite = sprite;
    }
    public void Interact(SelectableMask mask)
    {
        Selected = mask;
        SoundManager.Instance.PlaySound("PutMaskOn");

        _character.SetBool("Use", true);
        _mat.SetTexture("_MainTex", mask.Texture);
        _timer.CompleteTimeout();
    }

    public void TakeOff()
    {
        if (!Selected) return;

        Selected = null;
        _character.SetBool("Use", false);
        SoundManager.Instance.PlaySound("TakeMaskOff");
    }
}