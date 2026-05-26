using UnityEngine;

[System.Serializable]
public class DatosNivel
{
    [Tooltip("Velocidad de movimiento de los enemigos en esta oleada")]
    public float velocidadEnemigos = 2f;

    [Tooltip("Velocidad de los proyectiles del jugador en esta oleada")]
    public float velocidadProyectil = 10f;
}

public class GestorNiveles : MonoBehaviour
{
    [Header("Niveles configurables")]
    [Tooltip("Define aquí los parámetros de cada oleada. El sistema continúa infinitamente después del último.")]
    public DatosNivel[] nivelesDefinidos = new DatosNivel[]
    {
        new DatosNivel { velocidadEnemigos = 2f,   velocidadProyectil = 10f },
        new DatosNivel { velocidadEnemigos = 2.5f, velocidadProyectil = 11f },
        new DatosNivel { velocidadEnemigos = 3f,   velocidadProyectil = 12f },
        new DatosNivel { velocidadEnemigos = 3.5f, velocidadProyectil = 13f },
        new DatosNivel { velocidadEnemigos = 4f,   velocidadProyectil = 14f },
    };

    [Header("Escala infinita (después del último nivel definido)")]
    [Tooltip("Cuánto aumenta la velocidad de enemigos por oleada extra")]
    public float escalaVelocidadEnemigos = 0.4f;

    [Tooltip("Cuánto aumenta la velocidad de proyectil por oleada extra")]
    public float escalaVelocidadProyectil = 0.5f;

    [Header("Visual")]
    public GestorVisual gestorVisual;

    // Oleada actual (empieza en 1)
    public int OleadaActual { get; private set; } = 1;

    // ─── Propiedades calculadas ───────────────────────────────────────────────

    public virtual float VelocidadEnemigos => DatosActuales().velocidadEnemigos;
    public float VelocidadProyectil => DatosActuales().velocidadProyectil;
    public virtual int CantidadEnemigos => 3 + (OleadaActual - 1) * 2;
    public virtual int ProyectilesPorDisparo => 1 + (OleadaActual - 1) / 3;

    // ─── Inicialización ───────────────────────────────────────────────────────

    void Start()
    {
        if (gestorVisual != null)
            gestorVisual.AplicarVisualDeNivel(OleadaActual);
    }

    // ─── API pública ──────────────────────────────────────────────────────────

    public void AvanzarNivel()
    {
        OleadaActual++;

        if (gestorVisual != null)
            gestorVisual.AplicarVisualDeNivel(OleadaActual);

        Debug.Log($"[GestorNiveles] Nivel {OleadaActual} — Enemigos vel:{VelocidadEnemigos} | Proyectil vel:{VelocidadProyectil} | Balas:{ProyectilesPorDisparo}");
    }

    // ─── Privado ──────────────────────────────────────────────────────────────

    DatosNivel DatosActuales()
    {
        int indice = OleadaActual - 1;

        if (indice < nivelesDefinidos.Length)
            return nivelesDefinidos[indice];

        int oleadasExtra = indice - (nivelesDefinidos.Length - 1);
        DatosNivel ultimo = nivelesDefinidos[nivelesDefinidos.Length - 1];

        return new DatosNivel
        {
            velocidadEnemigos = ultimo.velocidadEnemigos + escalaVelocidadEnemigos * oleadasExtra,
            velocidadProyectil = ultimo.velocidadProyectil + escalaVelocidadProyectil * oleadasExtra
        };
    }
}