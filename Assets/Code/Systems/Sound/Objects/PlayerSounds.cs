using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("Sound Names")]
    [SerializeField] private string breath;
    private SerializableGuid breathId;

    private void Start()
    {
        PlayBreathLoop();
    }

    private void PlayBreathLoop()
    {
        SoundInstance breathInstance = SoundManager.Instance.PlaySound(breath);
        if(breathInstance.status == SoundInstance.STATUS.OK)
        {
            breathId = breathInstance.Id;
        }
    }
}