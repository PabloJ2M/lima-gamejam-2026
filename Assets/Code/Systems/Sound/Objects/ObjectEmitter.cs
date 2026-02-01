using FMODUnity;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour
{
    [SerializeField] private string soundName;
    private StudioEventEmitter emitter;

    private void Awake()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        SoundManager.Instance.InitializeEventEmitter(soundName, gameObject);
        emitter.Play();
    }
}