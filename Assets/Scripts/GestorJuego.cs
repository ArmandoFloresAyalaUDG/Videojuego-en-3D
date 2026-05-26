using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestorJuego : MonoBehaviour
{
    [Header("UI - HUD")]
    public TextMeshProUGUI textoOleada;
    public TextMeshProUGUI textoEnemigos;
    public TextMeshProUGUI textoRecord;

    [Header("UI - Game Over")]
    public GameObject panelGameOver;
    public TextMeshProUGUI textoFinal;

    private SpawnManager spawnManager;
    private int enemigosEliminados = 0;
    private int record = 0;

    void Start()
    {
        spawnManager = FindFirstObjectByType<SpawnManager>();
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
        if (spawnManager != null)
            textoOleada.text = "Oleada: " + spawnManager.oleadaActual;

        textoEnemigos.text = "Eliminados: " + enemigosEliminados;
        textoRecord.text = "Récord: " + record;
    }
}