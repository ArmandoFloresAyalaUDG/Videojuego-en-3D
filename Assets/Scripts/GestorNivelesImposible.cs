using UnityEngine;

public class GestorNivelesImposible : GestorNiveles
{
    // Enemigos: empieza en 50, suma 50 por oleada
    public override int CantidadEnemigos => 50 + (OleadaActual - 1) * 50;

    // Proyectiles: empieza en 2, suma 2 cada 10 oleadas
    public override int ProyectilesPorDisparo => 2 + ((OleadaActual - 1) / 10) * 2;

    // Velocidad enemigos: empieza en 10, suma 5 cada 2 oleadas
    public override float VelocidadEnemigos => 10f + ((OleadaActual - 1) / 2) * 5f;
}