using UnityEngine;
using UnityEngine.UI;

public class MaskHoldSystem : MonoBehaviour {
    [System.Serializable]
    public class MaskSlot {
        public KeyCode key;
        public GameObject maskFeedback;
    }

    [Header("UI")]
    [SerializeField] private Image fillImage;

    [Header("Masks")]
    public MaskSlot[] masks = new MaskSlot[3];

    [Header("Hold Settings")]
    public float holdDuration = 2f;

    private int _holdingMaskIndex = -1;
    private int _equippedMaskIndex = -1;
    private float _holdTimer;

    private void Start() {
        ResetSystem();
    }

    private void Update() {
        int pressedMaskIndex = GetPressedMaskIndex();

        if(pressedMaskIndex == -1) {
            ResetHold();
            return;
        }

        if(_holdingMaskIndex != pressedMaskIndex) {
            ResetHold();
            _holdingMaskIndex = pressedMaskIndex;
        }

        UpdateHold(_holdingMaskIndex);
    }

    private int GetPressedMaskIndex() {
        for(int i = 0; i < masks.Length; i++) {
            if(Input.GetKey(masks[i].key))
                return i;
        }

        return-1;
    }

    private void UpdateHold(int index) {
        _holdTimer += Time.deltaTime;
        fillImage.fillAmount = _holdTimer / holdDuration;

        if(_holdTimer >= holdDuration) {
            ResolveMaskAction(index);
            ResetHold();
        }
    }

    private void ResolveMaskAction(int index) {
        // Caso 1: no hay máscara → equipar
        if(_equippedMaskIndex == -1) {
            EquipMask(index);
            return;
        }

        // Caso 2: misma máscara → quitar
        if(_equippedMaskIndex == index) {
            UnequipMask();
            return;
        }

        // Caso 3: otra máscara → cambiar
        EquipMask(index);
    }

    private void EquipMask(int index) {
        UnequipMask();

        _equippedMaskIndex = index;
        masks[index].maskFeedback.SetActive(true);

        // OnMaskEquipped?.Invoke(index);
    }

    private void UnequipMask() {
        if(_equippedMaskIndex == -1)
            return;

        masks[_equippedMaskIndex].maskFeedback.SetActive(false);
        _equippedMaskIndex = -1;

        // OnMaskUnequipped?.Invoke();
    }

    private void ResetHold() {
        _holdTimer = 0f;
        _holdingMaskIndex = -1;
        fillImage.fillAmount = 0f;
    }

    public void ResetSystem() {
        ResetHold();
        UnequipMask();
    }
}