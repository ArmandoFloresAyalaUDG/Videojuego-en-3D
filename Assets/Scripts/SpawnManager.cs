using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabEnemigo;
    public float radioSpawn = 8f;

    [Header("Oleadas")]
    public int oleadaActual = 1;
    public int enemigosBaseXOleada = 3;
    public float tiempoEntreOleadas = 5f;

    private int enemigosVivos = 0;
    private bool esperandoOleada = false;

    void Start()
    {
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
        oleadaActual++;
        IniciarOleada();
        esperandoOleada = false;
    }

    void IniciarOleada()
    {
        int cantidad = enemigosBaseXOleada + (oleadaActual - 1) * 2;
        for (int i = 0; i < cantidad; i++)
        {
            SpawnEnemigo();
        }
    }

    void SpawnEnemigo()
    {
        float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 posicion = new Vector3(
            Mathf.Cos(angulo) * radioSpawn,
            1f,
            Mathf.Sin(angulo) * radioSpawn
        );
        Instantiate(prefabEnemigo, posicion, Quaternion.identity);
    }
}