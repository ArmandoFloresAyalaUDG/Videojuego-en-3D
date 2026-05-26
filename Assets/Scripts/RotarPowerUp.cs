using UnityEngine;

public class RotarPowerUp : MonoBehaviour
{
    public float velocidadRotacion = 90f;

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}