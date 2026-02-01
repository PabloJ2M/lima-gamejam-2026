using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] private float _recoveryTime;
    [SerializeField] private GameObject _container;

    private float _recovery;

    private void Update()
    {
        if (_recovery > 0)
            _recovery -= Time.deltaTime;
    }

    public bool SelectMask()
    {
        if (_recovery > 0) return false;
        _container.SetActive(true);
        return true;
    }
    public void DeselectMask()
    {
        _container.SetActive(false);
        _recovery = _recoveryTime;
    }
}
