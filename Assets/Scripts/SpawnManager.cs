using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Configuración de spawn")]
    public GameObject prefabEnemigo;
    public float radioSpawn = 8f;
    public float tiempoEntreOleadas = 5f;

    private GestorNiveles gestorNiveles;
    private int enemigosVivos = 0;
    private bool esperandoOleada = false;

    void Start()
    {
        gestorNiveles = FindFirstObjectByType<GestorNiveles>();

        if (gestorNiveles == null)
            Debug.LogError("[SpawnManager] No se encontró GestorNiveles en la escena.");

        IniciarOleada();
    }

    void Update()
    {
        enemigosVivos = GameObject.FindGameObjectsWithTag("Enemigo").Length;

        if (enemigosVivos == 0 && !esperandoOleada)
        {
            esperandoOleada = true;
            StartCoroutine(SiguienteOleada());
        }
    }

    IEnumerator SiguienteOleada()
    {
        yield return new WaitForSeconds(tiempoEntreOleadas);
        gestorNiveles?.AvanzarNivel();
        IniciarOleada();
        esperandoOleada = false;
    }

    void IniciarOleada()
    {
        int cantidad = gestorNiveles != null ? gestorNiveles.CantidadEnemigos : 3;

        for (int i = 0; i < cantidad; i++)
            SpawnEnemigo();
    }

    void SpawnEnemigo()
    {
        float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 posicion = new Vector3(
            Mathf.Cos(angulo) * radioSpawn,
            1f,
            Mathf.Sin(angulo) * radioSpawn
        );

        GameObject enemigo = Instantiate(prefabEnemigo, posicion, Quaternion.identity);

        // Pasar velocidad del nivel actual al enemigo
        EnemigoIA ia = enemigo.GetComponent<EnemigoIA>();
        if (ia != null && gestorNiveles != null)
            ia.velocidadBase = gestorNiveles.VelocidadEnemigos;
    }
}