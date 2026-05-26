using UnityEngine;

public class MovimientoProyectil : MonoBehaviour
{
    private float velocidad = 10f;
    private Vector3 direccion;

    public void Inicializar(Vector3 dir, float vel = 10f)
    {
        direccion = dir.normalized;
        velocidad = vel;
    }

    void Update()
    {
        transform.position += direccion * velocidad * Time.deltaTime;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            GestorJuego gestor = FindFirstObjectByType<GestorJuego>();
            if (gestor != null) gestor.RegistrarEliminacion();

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}