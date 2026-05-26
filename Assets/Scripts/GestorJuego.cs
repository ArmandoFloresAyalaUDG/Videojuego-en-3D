using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestorJuego : MonoBehaviour
{
    [Header("UI - HUD existente")]
    public TextMeshProUGUI textoOleada;
    public TextMeshProUGUI textoEnemigos;
    public TextMeshProUGUI textoRecord;

    [Header("UI - Panel Enemigos (izquierda)")]
    public TextMeshProUGUI panelEnemigos;

    [Header("UI - Panel Jugador (derecha)")]
    public TextMeshProUGUI panelJugador;

    [Header("UI - Game Over")]
    public GameObject panelGameOver;
    public TextMeshProUGUI textoFinal;

    private GestorNiveles gestorNiveles;
    private int enemigosEliminados = 0;
    private int record = 0;

    void Start()
    {
        gestorNiveles = FindFirstObjectByType<GestorNiveles>();
        record = PlayerPrefs.GetInt("Record", 0);
        panelGameOver.SetActive(false);
        ActualizarUI();
    }

    void Update()
    {
        ActualizarUI();
    }

    public void RegistrarEliminacion()
    {
        enemigosEliminados++;

        if (enemigosEliminados > record)
        {
            record = enemigosEliminados;
            PlayerPrefs.SetInt("Record", record);
            PlayerPrefs.Save();
        }

        ActualizarUI();
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        textoFinal.text = "Eliminados: " + enemigosEliminados + "  |  Récord: " + record;
        Time.timeScale = 0f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ActualizarUI()
    {
        if (gestorNiveles != null)
        {
            textoOleada.text = "Oleada: " + gestorNiveles.OleadaActual;

            panelEnemigos.text =
                "<b>— ENEMIGOS —</b>\n" +
                "Oleada: " + gestorNiveles.OleadaActual + "\n" +
                "Cantidad: " + gestorNiveles.CantidadEnemigos + "\n" +
                "Velocidad: " + gestorNiveles.VelocidadEnemigos.ToString("F1");

            panelJugador.text =
                "<b>— JUGADOR —</b>\n" +
                "Proyectiles: " + gestorNiveles.ProyectilesPorDisparo + "\n" +
                "Vel. proyectil: " + gestorNiveles.VelocidadProyectil.ToString("F1");
        }

        textoEnemigos.text = "Eliminados: " + enemigosEliminados;
        textoRecord.text = "Récord: " + record;
    }
}