using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Este script es el encargado de la generación del escenario

    public GameObject[] tilePrefabs;
    public float zSpawn = 0f;
    public float tileLength = 6f;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    int lastGenerated = 0;

    public Transform playerTransform;
    
    // El start genera una trozo de escenario vacío en primer lugar para no chocar nada mas empezar el juego
    void Start()
    {
        for(int i = 0; i <2; i++)
        {
            DrawTile(0);
        }

        for(int i = 2; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    // El update se ocupa de ir despawneando el escenario que deja de verse en pantalla a medida que el jugador avanza
    void Update()
    {
        if (playerTransform.position.z - 10 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    // Esta funcion existe para no permitir que se generen dos veces seguidas el mismo prefab de escenario, para evitar que el juego sea repetitivo.
    public void SpawnTile()
    {
        int tileIndex = Random.Range(0, tilePrefabs.Length);
        while (lastGenerated == tileIndex)
        {
            tileIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastGenerated = tileIndex;
        DrawTile(tileIndex);
    }

    // Esta función coloca los prefabs uno detrás de otro
    void DrawTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    // Esta función borra los prefabs
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
