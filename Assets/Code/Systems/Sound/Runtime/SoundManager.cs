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
    [SerializeField] public string defaultAmbience;
    [SerializeField] public string defaultMusic;

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

    private bool TryCreateInstance(string name, out EventInstance eventInstance)
    {
        if(!SoundLoader.TryGetEvent(name, out EventReference reference))
        {
            eventInstance = default;
            return false;
        }

        eventInstance = RuntimeManager.CreateInstance(reference);
        return true;
    }

    public StudioEventEmitter InitializeEventEmitter(string name, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject?.GetComponent<StudioEventEmitter>();
        
        if(emitter == null)
        {
            return null;
        }

        if (!SoundLoader.TryGetEvent(name, out EventReference reference))
        {
            return null;
        }

        emitter.EventReference = reference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    public SoundInstance PlaySound(string name)
    {
        if(!TryCreateInstance(name, out EventInstance instance))
        {
            return new SoundInstance
            {
                status = SoundInstance.STATUS.ERROR
            };
        }
        SerializableGuid guid = new SerializableGuid(Guid.NewGuid());

        eventInstances.Add(guid, instance);

        return new SoundInstance
        {
            Name = name,
            Id = guid,
            status = SoundInstance.STATUS.OK,
        };
    }

    public void StopSound(SerializableGuid id)
    {
        if(!eventInstances.TryGetValue(id, out EventInstance instance))
        {
            return;
        }

        eventInstances.Remove(id);
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }

    public void SetAmbience(string name)
    {
        if (setAmbience)
        {
            setAmbience = false;
            StopSound(ambienceId);
            ambienceId = default;
        }

        SoundInstance instance = PlaySound(name);
        if(instance.status == SoundInstance.STATUS.ERROR)
        {
            return;
        }

        setAmbience = true;
        ambienceId = instance.Id;
    }

    public void SetMusic(string name)
    {
        if (setMusic)
        {
            setMusic = false;
            StopSound(musicId);
            musicId = default;
        }

        SoundInstance instance = PlaySound(name);
        if (instance.status == SoundInstance.STATUS.ERROR)
        {
            return;
        }

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