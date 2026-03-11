public enum Paranoia
{
    None = 0,
    Exito = 1,
    Observado = 2,
    Tecnologia = 4
}
public enum SignalType
{
    Televisor,
    Ventilador,
    Ventanas,
    Radio,
    Pizarra,
    Fluorescentes
}

namespace UnityEngine.Gameplay
{
    public interface ISignalEmitter
    {
        float Duration { get; }
        void EmitteSignal();
        void EmitteFakeSignal();
    }
}