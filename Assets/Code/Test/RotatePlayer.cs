using FMODUnity;
using UnityEngine;
using UnityEngine.Windows;

public class RotatePlayer : MonoBehaviour
{
    public Transform cameraTransform;

    public float rotationSpeed = 6f;

    private bool resetSound = false;
    private bool playingSound = false;
    private SerializableGuid chairSoundId;

    [Header("Sounds")]
    [SerializeField] private EventReference[] chairSounds;
    [SerializeField] private float soundStopThreshold = .5f;
    [SerializeField] private float soundStartThreshold = 5f;
    private int direction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Direcci�n de la c�mara (PLANA)
        Vector3 camForward = cameraTransform.forward;

        camForward.y = 0f;

        camForward.Normalize();

        float diff = 0;

        if (camForward.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            diff = Quaternion.Angle(transform.rotation, targetRotation);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (diff < soundStopThreshold || Mathf.Sign(diff) != direction)
        {
            resetSound = true;
            if(playingSound) StopChairSound();
        }

        if (!playingSound && resetSound && diff > soundStartThreshold)
        {
            PlayChairSound();
            direction = diff > 0 ? 1 : -1;
        }

        /*Vector3 moveDir = camForward + camRight;

        // Movimiento
        //controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Rotaci�n suave hacia donde se mueve
        Quaternion targetRot = Quaternion.LookRotation(moveDir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );*/
    }

    private void PlayChairSound()
    {
        EventReference soundReference = chairSounds[Random.Range(0, chairSounds.Length)];
        SoundInstance instance = SoundManager.Instance.PlaySound(soundReference);
        chairSoundId = instance.Id;
        playingSound = true;
    }

    private void StopChairSound()
    {
        SoundManager.Instance.StopSound(chairSoundId);
        playingSound = false;
    }
}
