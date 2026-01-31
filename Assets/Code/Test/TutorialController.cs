using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    public GameObject camInit, camTutorial;
    public float time_to_off_lights;

    public UnityEvent onEvent1;
    public UnityEvent onEvent2;

    void Start()
    {
        camTutorial.SetActive(false);
        StartCoroutine(InitSequence());
    }

    IEnumerator InitSequence()
    {
        yield return new WaitForSeconds(2f);
        onEvent1?.Invoke();
        camInit.SetActive(false);
        camTutorial.SetActive(true);
        yield return new WaitForSeconds(time_to_off_lights);
        onEvent2?.Invoke();
    }
}
