using FMODUnity;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour
{
    [SerializeField] private EventReference soundReference;
    private StudioEventEmitter emitter;

    private void Awake()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        SoundManager.Instance.InitializeEventEmitter(soundReference, emitter);
        emitter.Play();
    }
}