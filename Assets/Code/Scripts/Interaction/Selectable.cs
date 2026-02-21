using UnityEngine;

namespace Gameplay.Interaction
{
    public abstract class Selectable : MonoBehaviour
    {
        [SerializeField] private GameObject _render;

        private const string _normalLayer = "Selectable", _selectedLayer = "Selected";

        protected virtual void OnEnable() => OnDeselect();
        protected virtual void Reset()
        {
            _render = gameObject;
            OnDeselect();
        }

        public abstract void OnInteract();
        public virtual void OnSelect() => _render.layer = LayerMask.NameToLayer(_selectedLayer);
        public virtual void OnDeselect() => _render.layer = LayerMask.NameToLayer(_normalLayer);
    }
}