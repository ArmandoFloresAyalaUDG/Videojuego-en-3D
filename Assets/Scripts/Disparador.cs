using UnityEngine;
using UnityEngine.InputSystem;

public class Disparador : MonoBehaviour
{
    public GameObject prefabProyectil;
    private float cadencia = 0.4f;
    private float tiempoUltimoDisparo = 0f;
    private SpawnManager spawnManager;

    void Start()
    {
        spawnManager = FindFirstObjectByType<SpawnManager>();
    }

    void Update()
    {
        ActualizarNivel();

        if (Mouse.current.leftButton.isPressed && Time.time >= tiempoUltimoDisparo + cadencia)
        {
            int oleada = spawnManager != null ? spawnManager.oleadaActual : 1;

            if (oleada >= 5)
                Disparar(3);
            else
                Disparar(1);

            tiempoUltimoDisparo = Time.time;
        }
    }

    void ActualizarNivel()
    {
        if (spawnManager == null) return;

        int oleada = spawnManager.oleadaActual;

        if (oleada >= 5)
            cadencia = 0.2f;       // Oleada 5+: ráfaga triple rápida
        else if (oleada >= 3)
            cadencia = 0.25f;      // Oleada 3-4: más rápida
        else
            cadencia = 0.4f;       // Oleada 1-2: normal
    }

    public void Disparar(int cantidadBalas)
    {
        Vector3 direccion = ObtenerDireccionCursor();

        if (cantidadBalas == 1)
        {
            SpawnProyectil(direccion, 0f);
        }
        else if (cantidadBalas == 3)
        {
            SpawnProyectil(direccion, -15f);
            SpawnProyectil(direccion, 0f);
            SpawnProyectil(direccion, 15f);
        }
    }

    void SpawnProyectil(Vector3 dir, float offsetAngulo)
    {
        Quaternion rotacion = Quaternion.Euler(0f, offsetAngulo, 0f);
        Vector3 dirFinal = rotacion * dir;
        GameObject p = Instantiate(prefabProyectil, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        p.GetComponent<MovimientoProyectil>().Inicializar(dirFinal);
    }

    Vector3 ObtenerDireccionCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plano = new Plane(Vector3.up, new Vector3(0f, 1f, 0f));
        if (plano.Raycast(ray, out float distancia))
        {
            Vector3 puntoMundo = ray.GetPoint(distancia);
            Vector3 dir = puntoMundo - transform.position;
            dir.y = 0f;
            return dir.normalized;
        }
        return transform.forward;
    }
}