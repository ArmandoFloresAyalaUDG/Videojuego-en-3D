using UnityEngine;
using UnityEngine.UI;

public class PowerUpVelocidad : MonoBehaviour
{
    [Header("Configuración")]
    public float multiplicadorVelocidad = 2.5f;
    public float duracion = 3f;
    public float tiempoReaparicion = 15f;

    [Header("Referencias")]
    public Slider sliderVelocidad;
    public TrailRenderer estela;

    private bool activo = false;
    private float tiempoRestante = 0f;
    private float tiempoParaReaparecer = 0f;
    private bool esperandoReaparicion = false;
    private MovimientoJugador3D jugadorScript;
    private float velocidadOriginal;

    private MeshRenderer meshRenderer;
    private Collider col;

    // Límites de la arena (ajustados al Plane escala 5,1,5)
    private float limiteArena = 4f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Jugador") && !activo && !esperandoReaparicion)
        {
            jugadorScript = otro.GetComponent<MovimientoJugador3D>();
            if (jugadorScript == null) return;

            velocidadOriginal = jugadorScript.velocidad;
            jugadorScript.velocidad *= multiplicadorVelocidad;

            tiempoRestante = duracion;
            activo = true;

            if (estela != null) estela.enabled = true;
            if (sliderVelocidad != null) sliderVelocidad.gameObject.SetActive(true);

            meshRenderer.enabled = false;
            col.enabled = false;

            tiempoParaReaparecer = tiempoReaparicion;
            esperandoReaparicion = true;
        }
    }

    void Update()
    {
        // Gestionar efecto de velocidad
        if (activo)
        {
            tiempoRestante -= Time.deltaTime;
            float progreso = tiempoRestante / duracion;

            if (sliderVelocidad != null)
                sliderVelocidad.value = progreso;

            if (tiempoRestante <= 0f)
            {
                if (jugadorScript != null)
                    jugadorScript.velocidad = velocidadOriginal;

                if (estela != null) estela.enabled = false;
                if (sliderVelocidad != null) sliderVelocidad.gameObject.SetActive(false);

                activo = false;
            }
        }

        // Gestionar reaparición
        if (esperandoReaparicion)
        {
            tiempoParaReaparecer -= Time.deltaTime;

            if (tiempoParaReaparecer <= 0f)
            {
                // Posición aleatoria dentro de la arena
                float x = Random.Range(-limiteArena, limiteArena);
                float z = Random.Range(-limiteArena, limiteArena);
                transform.position = new Vector3(x, 0.5f, z);

                meshRenderer.enabled = true;
                col.enabled = true;
                esperandoReaparicion = false;
            }
        }
    }
}