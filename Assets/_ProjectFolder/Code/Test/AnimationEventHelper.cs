using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent onEvent1;
    public UnityEvent onEvent2;
    public UnityEvent onEvent3;
    public UnityEvent onEvent4;

    public void OnEvent1()
    {
        onEvent1?.Invoke();
    }
    public void OnEvent2()
    {
        onEvent2?.Invoke();
    }
    public void OnEvent3()
    {
        onEvent3?.Invoke();
    }
    public void OnEvent4()
    {
        onEvent4?.Invoke();
    }
}
