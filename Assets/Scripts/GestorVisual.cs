using UnityEngine;

public class GestorVisual : MonoBehaviour
{
    [Header("Cámara")]
    public Camera camaraJuego;

    [Header("Material del jugador")]
    public Material materialJugador;

    [Header("Colores de fondo por nivel")]
    public Color[] coloresFondo = new Color[]
    {
        new Color(0.53f, 0.81f, 0.98f), // Nivel 1 - Día
        new Color(0.98f, 0.65f, 0.23f), // Nivel 2 - Atardecer
        new Color(0.15f, 0.15f, 0.35f), // Nivel 3 - Noche
        new Color(0.05f, 0.05f, 0.15f), // Nivel 4 - Medianoche
        new Color(0.02f, 0.00f, 0.08f)  // Nivel 5+ - Abismo
    };

    [Header("Colores del jugador por nivel")]
    public Color[] coloresJugador = new Color[]
    {
        new Color(0.00f, 0.71f, 1.00f), // Nivel 1 - Azul
        new Color(0.00f, 1.00f, 0.50f), // Nivel 2 - Verde
        new Color(1.00f, 0.84f, 0.00f), // Nivel 3 - Dorado
        new Color(1.00f, 0.40f, 0.00f), // Nivel 4 - Naranja
        new Color(1.00f, 0.00f, 0.00f)  // Nivel 5+ - Rojo
    };

    public void AplicarVisualDeNivel(int nivel)
    {
        int indice = Mathf.Clamp(nivel - 1, 0, coloresFondo.Length - 1);

        if (camaraJuego != null)
            camaraJuego.backgroundColor = coloresFondo[indice];

        if (materialJugador != null)
            materialJugador.color = coloresJugador[indice];
    }
}