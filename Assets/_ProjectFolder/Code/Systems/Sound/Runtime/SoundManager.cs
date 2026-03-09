using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public enum BUS
    {
        MASTER,
        AMBIENCE,
        MUSIC,
        SFX,
    }

    [Header("Default Volume")]
    [SerializeField] private float defaultMasterVolume = 1.0f;
    [SerializeField] private float defaultAmbienceVolume = 1.0f;
    [SerializeField] private float defaultMusicVolume = 1.0f;
    [SerializeField] private float defaultSFXVolume = 1.0f;

    [Header("Default Sounds")]
    [SerializeField] public EventReference defaultAmbience;
    [SerializeField] public EventReference defaultMusic;

    private Bus masterBus;
    private Bus ambienceBus;
    private Bus musicBus;
    private Bus sfxBus;

    private Dictionary<SerializableGuid, EventInstance> eventInstances = new();
    private List<StudioEventEmitter> eventEmitters = new();

    private bool setAmbience = false;
    private SerializableGuid ambienceId;
    private bool setMusic = false;
    private SerializableGuid musicId;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
#if UNITY_EDITOR
            Debug.LogError("There's more than one Sound Manager in the scene");
#endif
            Destroy(gameObject);
        }
        Instance = this;

        masterBus = RuntimeManager.GetBus("bus:/");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Start()
    {
        SetBusVolume(BUS.MASTER, defaultMasterVolume);
        SetBusVolume(BUS.AMBIENCE, defaultAmbienceVolume);
        SetBusVolume(BUS.MUSIC, defaultMusicVolume);
        SetBusVolume(BUS.SFX, defaultSFXVolume);

        SetAmbience(defaultAmbience);
        SetMusic(defaultMusic);
    }

    public void SetBusVolume(BUS busType, float volume)

    {
        volume = Mathf.Clamp01(volume);
        Bus bus = busType switch
        {
            BUS.MASTER => masterBus,
            BUS.AMBIENCE => ambienceBus,
            BUS.MUSIC => musicBus,
            BUS.SFX => sfxBus,
            _ => masterBus
        };
        bus.setVolume(volume);
    }

    private EventInstance CreateInstance(EventReference reference)
    {
        if(reference.IsNull) return default;
        return RuntimeManager.CreateInstance(reference);
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference reference, StudioEventEmitter emitter)
    {
        emitter.EventReference = reference;
        if(!eventEmitters.Contains(emitter)) eventEmitters.Add(emitter);

        emitter.Play();
        return emitter;
    }

    public SoundInstance PlaySound(EventReference reference)
    {
        EventInstance instance = CreateInstance(reference);

        if(!instance.isValid()) return null;

        SerializableGuid guid = new SerializableGuid(Guid.NewGuid());

        eventInstances.Add(guid, instance);
        instance.start();

        return new SoundInstance
        {
            Name = name,
            Id = guid,
        };
    }

    public void StopSound(SerializableGuid id)
    {
        if(id.IsEmpty() || !eventInstances.TryGetValue(id, out EventInstance instance))
        {
            return;
        }

        eventInstances.Remove(id);
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }

    public void StopAmbience()
    {
        if (setAmbience)
        {
            setAmbience = false;
            StopSound(ambienceId);
            ambienceId = default;
        }
    }

    public void SetAmbience(EventReference reference)
    {
        StopAmbience();

        SoundInstance instance = PlaySound(reference);

        if(instance == null) return;

        setAmbience = true;
        ambienceId = instance.Id;
    }

    public void StopMusic()
    {
        if (setMusic)
        {
            setMusic = false;
            StopSound(musicId);
            musicId = default;
        }
    }

    public void SetMusic(EventReference reference)
    {
        StopMusic();

        SoundInstance instance = PlaySound(reference);

        if(instance == null) return;

        setMusic = true;
        musicId = instance.Id;
    }

    public void SetFloatParameterInInstance(SerializableGuid id, string parameterName, float value)
    {
        if (!eventInstances.TryGetValue(id, out EventInstance instance))
        {
            return;
        }

        instance.setParameterByName(name, value);
    }

    private void OnDestroy()
    {
        foreach(var eventInstance in eventInstances.Values) {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        foreach(var emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }
}