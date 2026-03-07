using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private CursorHandler _cursorHandler;
    [SerializeField] private GameObject _pauseScreen, _loseScreen;

    public void TriggerLoseState()
    {
        _cursorHandler.CursorUnlock();
        _pauseScreen.SetActive(false);
        _loseScreen.SetActive(true);
    }
}