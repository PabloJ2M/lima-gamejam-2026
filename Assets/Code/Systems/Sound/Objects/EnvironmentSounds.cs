using FMODUnity;
using UnityEngine;

public class EnvironmentSounds : MonoBehaviour
{
    [Header("Ambience")]
    [SerializeField] private EventReference globalAmbience;

    [Header("Music")]
    [SerializeField] private EventReference endGameMusic;
    [SerializeField] private EventReference gameOverMusic;
    [SerializeField] private EventReference winMusic;

    public void SetGlobalAmbience()
    {
        SoundManager.Instance.SetAmbience(globalAmbience);
    }

    public void SetEndGameMusic()
    {
        SoundManager.Instance.SetMusic(endGameMusic);
    }

    public void SetGameOverMusic()
    {
        SoundManager.Instance.SetMusic(gameOverMusic);
    }

    public void SetWinMusic()
    {
        SoundManager.Instance.SetMusic(winMusic);
    }
}