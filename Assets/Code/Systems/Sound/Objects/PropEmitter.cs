using FMODUnity;
using UnityEngine;

public class PropEmitter : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _emitter;
    [SerializeField] private string[] sounds;

    public void Play()
    {
        string soundName = sounds[Random.Range(0, sounds.Length)];
        SoundManager.Instance.InitializeEventEmitter(soundName, _emitter);
    }
}