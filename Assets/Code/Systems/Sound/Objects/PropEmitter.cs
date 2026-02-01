using FMODUnity;
using UnityEngine;

public class PropEmitter : MonoBehaviour
{
    [SerializeField] private string[] sounds;
    [SerializeField] private StudioEventEmitter emitter;

    private void Awake()
    {
        if (emitter != null) return;

        emitter = GetComponent<StudioEventEmitter>();
        if(emitter == null)
        {
            emitter = gameObject.AddComponent<StudioEventEmitter>();
        }
    }

    public void Play()
    {
        string soundName = sounds[Random.Range(0, sounds.Length)];
        SoundManager.Instance.InitializeEventEmitter(soundName, emitter);
    }
}