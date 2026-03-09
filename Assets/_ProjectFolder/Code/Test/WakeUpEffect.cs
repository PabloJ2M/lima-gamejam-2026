using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WakeUpEffect : MonoBehaviour
{
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private Animator _imgFade;
    [SerializeField] private float _duration = 3f;

    private float _endVignetteIntensity;
    private float _endDepthOfFieldFocus;

    private Vignette _vignette;
    private DepthOfField _depthOfField;

    private void Awake()
    {
        if (_globalVolume.profile.TryGet(out _vignette))
        {
            _endVignetteIntensity = _vignette.intensity.value;
            _vignette.intensity.value = 1f;
        }
        if (_globalVolume.profile.TryGet(out _depthOfField))
        {
            _endDepthOfFieldFocus = _depthOfField.focusDistance.value;
            _depthOfField.focusDistance.value = 0.1f;
        }
    }
    private void OnDestroy()
    {
        _vignette.intensity.value = _endVignetteIntensity;
        _depthOfField.focusDistance.value = _endDepthOfFieldFocus;
    }

    public void StartComponents() => StartCoroutine(WakeUpRoutine());
    public void SetFocusDistance() => StartCoroutine(FocusRoutine());

    private IEnumerator WakeUpRoutine()
    {
        _imgFade.Play("fade-in");

        float time = 0f;

        float startVignette = _vignette.intensity.value;
        float startFocus = _depthOfField.focusDistance.value;

        while (time < _duration)
        {
            time += Time.deltaTime;
            float t = time / _duration;

            _vignette.intensity.value = Mathf.Lerp(startVignette, _endVignetteIntensity, t);

            yield return null;
        }

        _vignette.intensity.value = _endVignetteIntensity;
    }
    private IEnumerator FocusRoutine()
    {
        float time = 0f;
        float startFocus = _depthOfField.focusDistance.value;

        while (time < 2f)
        {
            time += Time.deltaTime;
            float t = time / _duration;

            _depthOfField.focusDistance.value = Mathf.Lerp(startFocus, _endDepthOfFieldFocus, t);

            yield return null;
        }

        _depthOfField.focusDistance.value = _endDepthOfFieldFocus;
    }
}