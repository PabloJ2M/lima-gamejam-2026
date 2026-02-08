using FMODUnity;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Sound Names")]
    [SerializeField] private EventReference breath;
    private SerializableGuid breathId;

    public void PlayBreathLoop()
    {
        SoundInstance breathInstance = SoundManager.Instance.PlaySound(breath);
        breathId = breathInstance.Id;
    }
}