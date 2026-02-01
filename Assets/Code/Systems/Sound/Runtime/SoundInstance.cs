public struct SoundInstance
{
    public enum STATUS
    {
        OK,
        ERROR,
    }

    public STATUS status;
    public SerializableGuid Id;
    public string Name;
}