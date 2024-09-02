using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour
{
    // Public variables to configure in the inspector
    public GameObject tilePrefab; // Drag your tile prefab here
    public int tileCount; // Set the number of tiles to spawn

    private void Start()
    {
        SpawnTile();
    }

    public void SpawnTile()
    {
        for (int spawnedTiles = 0; spawnedTiles < tileCount; spawnedTiles++)
        {
            // Instantiate the tile 
            GameObject newTile = Instantiate(tilePrefab, transform.position, Quaternion.identity);

            // Set the new tile as a child of the spawner
            newTile.transform.SetParent(transform);
            // Match the scale of the new tile to the spawner
            newTile.transform.localScale = transform.localScale;

            // Optionally, adjust the local position of the new tile if needed
            newTile.transform.localPosition = Vector3.zero; // Places it exactly on top
        }
    }
}
