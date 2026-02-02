using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private float _timeToSkipTutorial;
    [SerializeField] private float time_to_off_lights;

    public UnityEvent onEvent1;
    public UnityEvent onEvent2;

    private void Start() => StartCoroutine(InitSequence());
    private IEnumerator InitSequence()
    {
        yield return new WaitForSeconds(_timeToSkipTutorial);
        onEvent1?.Invoke();
        yield return new WaitForSeconds(time_to_off_lights);
        onEvent2?.Invoke();
    }
}
