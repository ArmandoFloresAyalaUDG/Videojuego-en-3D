using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - jugador.position;
    }

    void LateUpdate()
    {
        Vector3 nuevaPosicion = jugador.position + offset;
        transform.position = new Vector3(nuevaPosicion.x, transform.position.y, nuevaPosicion.z);
    }
}