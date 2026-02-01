using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Sound Library", menuName = "Sound Library")]
public class SoundLibrary : ScriptableObject
{
    public SoundReference[] References;

    public List<SoundReference> ToList() => References.ToList();
}