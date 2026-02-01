using UnityEngine;

public class RotateY : MonoBehaviour
{
    public float velocidad = 90f; // grados por segundo

    private bool stop;

    void Update()
    {
        if(stop) return;

        transform.Rotate(0f, velocidad * Time.deltaTime, 0f);
    }

    public void SetActive(bool value)
    {
        stop = value;
    }
}
