using System;
using UnityEngine;

[Flags]
public enum Paranoia
{
    Exito = 1,
    Observado = 2,
    Tecnologia = 4
}

[CreateAssetMenu(fileName = "Signal", menuName = "Signals/Signal")]
public class Signal : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Paranoia _paranoia;

    public Paranoia Paranoia => _paranoia;
}
