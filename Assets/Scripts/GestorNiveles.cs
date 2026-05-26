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
        new DatosNivel { velocidadEnemigos = 2f,  velocidadProyectil = 10f },
        new DatosNivel { velocidadEnemigos = 2.5f, velocidadProyectil = 11f },
        new DatosNivel { velocidadEnemigos = 3f,  velocidadProyectil = 12f },
        new DatosNivel { velocidadEnemigos = 3.5f, velocidadProyectil = 13f },
        new DatosNivel { velocidadEnemigos = 4f,  velocidadProyectil = 14f },
    };

    [Header("Escala infinita (después del último nivel definido)")]
    [Tooltip("Cuánto aumenta la velocidad de enemigos por oleada extra")]
    public float escalaVelocidadEnemigos = 0.4f;

    [Tooltip("Cuánto aumenta la velocidad de proyectil por oleada extra")]
    public float escalaVelocidadProyectil = 0.5f;

    // Oleada actual (empieza en 1)
    public int OleadaActual { get; private set; } = 1;

    // ─── Propiedades calculadas ───────────────────────────────────────────────

    /// <summary>Velocidad de enemigos para la oleada actual.</summary>
    public float VelocidadEnemigos => DatosActuales().velocidadEnemigos;

    /// <summary>Velocidad de proyectiles para la oleada actual.</summary>
    public float VelocidadProyectil => DatosActuales().velocidadProyectil;

    /// <summary>Cantidad de enemigos en la oleada actual (3 + 2*(oleada-1)).</summary>
    public int CantidadEnemigos => 3 + (OleadaActual - 1) * 2;

    /// <summary>Proyectiles por disparo: 1 + 1 cada 3 oleadas.</summary>
    public int ProyectilesPorDisparo => 1 + (OleadaActual - 1) / 3;

    // ─── API pública ──────────────────────────────────────────────────────────

    /// <summary>Llamar cuando una oleada termina para avanzar al siguiente nivel.</summary>
    public void AvanzarNivel()
    {
        OleadaActual++;
        Debug.Log($"[GestorNiveles] Nivel {OleadaActual} — Enemigos vel:{VelocidadEnemigos} | Proyectil vel:{VelocidadProyectil} | Balas:{ProyectilesPorDisparo}");
    }

    // ─── Privado ──────────────────────────────────────────────────────────────

    DatosNivel DatosActuales()
    {
        int indice = OleadaActual - 1;

        if (indice < nivelesDefinidos.Length)
            return nivelesDefinidos[indice];

        // Oleada más allá de los niveles definidos → escalar infinitamente
        int oleadasExtra = indice - (nivelesDefinidos.Length - 1);
        DatosNivel ultimo = nivelesDefinidos[nivelesDefinidos.Length - 1];

        return new DatosNivel
        {
            velocidadEnemigos = ultimo.velocidadEnemigos + escalaVelocidadEnemigos * oleadasExtra,
            velocidadProyectil = ultimo.velocidadProyectil + escalaVelocidadProyectil * oleadasExtra
        };
    }
}