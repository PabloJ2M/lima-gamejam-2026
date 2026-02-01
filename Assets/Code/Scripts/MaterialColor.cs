using UnityEngine;

public class MaterialColor : MonoBehaviour
{
    [SerializeField] private int _index;
    private const string _id = "_EMISSION";
    private Material _material;

    private void Awake() => _material = GetComponent<MeshRenderer>().materials[_index];

    public void DisplayColor(bool enable)
    {
        if (enable)
            _material.EnableKeyword(_id);
        else
            _material.DisableKeyword(_id);
    }
}