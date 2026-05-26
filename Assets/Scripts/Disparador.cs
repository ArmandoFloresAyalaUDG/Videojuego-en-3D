using UnityEngine;
using UnityEngine.InputSystem;

public class Disparador : MonoBehaviour
{
    public GameObject prefabProyectil;

    private float cadencia = 0.4f;
    private float tiempoUltimoDisparo = 0f;
    private GestorNiveles gestorNiveles;

    void Start()
    {
        gestorNiveles = FindFirstObjectByType<GestorNiveles>();
    }

    void Update()
    {
        ActualizarCadencia();

        if (Mouse.current.leftButton.isPressed && Time.time >= tiempoUltimoDisparo + cadencia)
        {
            int balas = gestorNiveles != null ? gestorNiveles.ProyectilesPorDisparo : 1;
            Disparar(balas);
            tiempoUltimoDisparo = Time.time;
        }
    }

    void ActualizarCadencia()
    {
        if (gestorNiveles == null) return;

        int oleada = gestorNiveles.OleadaActual;

        if (oleada >= 5)
            cadencia = 0.2f;
        else if (oleada >= 3)
            cadencia = 0.25f;
        else
            cadencia = 0.4f;
    }

    public void Disparar(int cantidadBalas)
    {
        Vector3 direccion = ObtenerDireccionCursor();
        float velocidad = gestorNiveles != null ? gestorNiveles.VelocidadProyectil : 10f;

        if (cantidadBalas == 1)
        {
            SpawnProyectil(direccion, 0f, velocidad);
        }
        else
        {
            float offsetBase = 15f;
            int mitad = cantidadBalas / 2;

            for (int i = 0; i < cantidadBalas; i++)
            {
                float offset = (i - mitad) * offsetBase;
                SpawnProyectil(direccion, offset, velocidad);
            }
        }
    }

    void SpawnProyectil(Vector3 dir, float offsetAngulo, float velocidad)
    {
        Quaternion rotacion = Quaternion.Euler(0f, offsetAngulo, 0f);
        Vector3 dirFinal = rotacion * dir;
        GameObject p = Instantiate(prefabProyectil, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        MovimientoProyectil mp = p.GetComponent<MovimientoProyectil>();
        if (mp != null) mp.Inicializar(dirFinal, velocidad);
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