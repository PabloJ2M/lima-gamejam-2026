using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Interaction
{
    public class RayInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private LayerMask _interactables;
        [SerializeField] private InputActionReference _triggerAction;

        private Selectable _currentSelected;

        private void Start() => _triggerAction.action.performed += Interact;

        private void Update()
        {
            Ray ray = new(_camera.position, _camera.forward);

            //Deselect Object
            if (!Physics.Raycast(ray, out RaycastHit hit, 10, _interactables, QueryTriggerInteraction.Ignore))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                ChangeSelection(null);
                return;
            }

            //Select New Object
            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            if (hit.collider.TryGetComponent(out Selectable newSelectable))
                ChangeSelection(newSelectable);
            else
                ChangeSelection(null);
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            _currentSelected?.OnInteract();
        }
        private void ChangeSelection(Selectable newSelectable)
        {
            if (_currentSelected == newSelectable) return;
            _currentSelected?.OnDeselect();

            _currentSelected = newSelectable;
            _currentSelected?.OnSelect();
        }
    }
}