using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private float _timeToSkipTutorial;
    [SerializeField] private float time_to_off_lights;

    public UnityEvent onEvent1;
    public UnityEvent onEvent2;

    private bool _isTutorialComplete;

    private void Start() => StartCoroutine(TutorialSequence());

    public void SkipTutorial()
    {
        if (_isTutorialComplete) return;
        onEvent1?.Invoke();
        StopAllCoroutines();
        StartCoroutine(GameSequence());
    }

    private IEnumerator TutorialSequence()
    {
        yield return new WaitForSeconds(_timeToSkipTutorial);
        onEvent1?.Invoke();
        StartCoroutine(GameSequence());
    }
    private IEnumerator GameSequence()
    {
        _isTutorialComplete = true;
        yield return new WaitForSeconds(time_to_off_lights);
        onEvent2?.Invoke();
    }
}