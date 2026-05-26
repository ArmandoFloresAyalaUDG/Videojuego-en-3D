using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public float velocidadBase = 2f;
    public float velocidadEmergencia = 3f;

    private Transform jugador;
    private float velocidadFinal;
    private bool emergiendo = true;
    private float alturaObjetivo = 1f;

    void Start()
    {
        GameObject obj = GameObject.FindWithTag("Jugador");
        if (obj != null) jugador = obj.transform;

        SpawnManager sm = FindFirstObjectByType<SpawnManager>();
        if (sm != null)
            velocidadFinal = velocidadBase + (sm.oleadaActual - 1) * 0.3f;
        else
            velocidadFinal = velocidadBase;

        // Aparece bajo el suelo
        transform.position = new Vector3(transform.position.x, -2f, transform.position.z);
    }

    void Update()
    {
        if (emergiendo)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, alturaObjetivo, transform.position.z),
                velocidadEmergencia * Time.deltaTime
            );

            if (Mathf.Abs(transform.position.y - alturaObjetivo) < 0.05f)
            {
                transform.position = new Vector3(transform.position.x, alturaObjetivo, transform.position.z);
                emergiendo = false;
            }
            return;
        }

        if (jugador == null) return;

        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0f;
        transform.position += direccion * velocidadFinal * Time.deltaTime;
    }
}