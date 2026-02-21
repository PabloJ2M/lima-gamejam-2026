using UnityEngine;

namespace Gameplay.Interaction
{
    public class SelectableMask : Selectable
    {
        [SerializeField] private Paranoia _paranoia;
        [SerializeField] private Texture _texture;
        [SerializeField] private Sprite _sprite;

        private MaskSelector _selectorManager;

        public Paranoia Paranoia => _paranoia;
        public Texture Texture => _texture;
        public Sprite Sprite => _sprite;

        private void Awake() => _selectorManager = GetComponentInParent<MaskSelector>();

        public override void OnInteract() => _selectorManager.Interact(this);
    }
}