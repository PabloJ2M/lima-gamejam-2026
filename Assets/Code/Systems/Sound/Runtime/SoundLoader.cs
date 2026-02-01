using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public static class SoundLoader
{
    public static Dictionary<string, EventReference> sounds = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialise()
    {
        SoundLibrary[] libries = Resources.LoadAll<SoundLibrary>("");

        List<SoundReference> allSoundReferences = new();

        foreach(SoundLibrary library in libries)
        {
            allSoundReferences.AddRange(library.ToList());
        }

        SortReferences(allSoundReferences);

#if UNITY_EDITOR
        Debug.Log("Sound Loaded!");
#endif
    }

    private static void SortReferences(List<SoundReference> soundReferences)
    {
        sounds.Clear();
        
        foreach(SoundReference reference in soundReferences)
        {
            if(sounds.ContainsKey(reference.Name))
            {
#if UNITY_EDITOR 
                Debug.LogWarning($"Duplicate Sound Reference Name at: {reference.Name}");
#endif
                continue;
            }

            sounds.Add(reference.Name, reference.SoundEvent);
        }

    }

    public static bool TryGetEvent(string name, out EventReference reference)
    {
        if(!sounds.TryGetValue(name, out reference))
        {
            return false;
        }
        return true;
    }
}