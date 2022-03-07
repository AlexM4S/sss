using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public float zSpawn = 0f;
    public float tileLength = 6f;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    int lastGenerated = 0;

    public Transform playerTransform;
    

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
    void Update()
    {
        if (playerTransform.position.z - 10 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

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

    void DrawTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
