using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WakeUpEffect : MonoBehaviour
{
    public Volume globalVolume;
    public Image imgFade;
    public float duration = 3f;

    public float endVignetteIntensity;
    public float endDepthOfFieldFocus;

    private Vignette vignette;
    private DepthOfField depthOfField;

    void Start()
    {
        StartComponents();
    }

    void StartComponents()
    {
        if (globalVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 1f;
        }

        if(globalVolume.profile.TryGet(out depthOfField))
        {
            depthOfField.focusDistance.value = 0.1f;
        }

        StartCoroutine(WakeUpRoutine());
    }

    IEnumerator WakeUpRoutine()
    {
        imgFade.GetComponent<Animator>().Play("fade-in");

        float time = 0f;

        float startVignette = vignette.intensity.value;
        float startFocus = depthOfField.focusDistance.value;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            vignette.intensity.value = Mathf.Lerp(startVignette, endVignetteIntensity, t);

            yield return null;
        }

        vignette.intensity.value = endVignetteIntensity;
    }

    public void SetFocusDistance()
    {
        StartCoroutine(FocusRoutine());
    }

    IEnumerator FocusRoutine()
    {
        float time = 0f;
        float startFocus = depthOfField.focusDistance.value;

        while (time < 2f)
        {
            time += Time.deltaTime;
            float t = time / duration;

            depthOfField.focusDistance.value = Mathf.Lerp(startFocus, endDepthOfFieldFocus, t);

            yield return null;
        }

        depthOfField.focusDistance.value = endDepthOfFieldFocus;
    }
}
