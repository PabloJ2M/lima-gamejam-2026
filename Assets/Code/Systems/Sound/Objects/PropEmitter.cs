using FMODUnity;
using UnityEngine;

public class PropEmitter : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _emitter;
    [SerializeField] private EventReference[] sounds;

    public void Play()
    {
        EventReference soundReference = sounds[Random.Range(0, sounds.Length)];
        SoundManager.Instance.InitializeEventEmitter(soundReference, _emitter);
    }
}