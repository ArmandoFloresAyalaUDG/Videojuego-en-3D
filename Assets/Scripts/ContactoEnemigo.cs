using UnityEngine;

public class ContactoEnemigo : MonoBehaviour
{
    private GestorJuego gestor;

    void Start()
    {
        gestor = FindFirstObjectByType<GestorJuego>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            if (gestor != null) gestor.GameOver();
        }
    }
}