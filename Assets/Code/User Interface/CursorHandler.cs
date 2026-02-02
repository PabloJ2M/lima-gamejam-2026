using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    private void Start() => CursorLock();

    public void CursorLock() => SetCursorStatus(false);
    public void CursorUnlock() => SetCursorStatus(true);

    private void SetCursorStatus(bool isVisible)
    {
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}