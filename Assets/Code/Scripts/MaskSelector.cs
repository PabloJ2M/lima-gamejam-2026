using UnityEngine;
using Gameplay.Interaction;

public class MaskSelector : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Material _mat;

    public SelectableMask Selected { get; private set; }

    public void Interact(SelectableMask mask)
    {
        Selected = mask;
        _anim.SetBool("Use", true);
        _mat.SetTexture("_MainTex", mask.Texture);
        SoundManager.Instance.PlaySound("PutMaskOn");
    }

    public void TakeOff()
    {
        if (!Selected) return;

        Selected = null;
        _anim.SetBool("Use", false);
        SoundManager.Instance.PlaySound("TakeMaskOff");
    }
}