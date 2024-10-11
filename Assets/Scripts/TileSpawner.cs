using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab; // The prefab for the tile to be spawned
    public int tileCount = 4; // Counter representing the remaining number of tiles
    public TextMeshProUGUI tileCounterText; // TextMeshProUGUI to display the remaining tile count

    private void Start()
    {
        // Update the remaining tile count
        UpdateTileCounter();

        // Spawn the tiles
        SpawnTiles();
    }

    public void SpawnTiles()
    {
        Debug.Log("SpawnTiles called!");

        // Spawn multiple tiles based on TileCount
        for (int i = 0; i < tileCount; i++)
        {
            GameObject newTile = Instantiate(tilePrefab, transform.position, Quaternion.identity);

            // Set the TileSpawner as the parent of the spawned tile
            newTile.transform.SetParent(this.transform, false);

            // Reset local coordinates to (0, 0, 0)
            newTile.transform.localPosition = Vector3.zero;

            // Pass the TileSpawner reference to the DraggableTile script
            DraggableTile draggableTile = newTile.GetComponent<DraggableTile>();
            if (draggableTile != null)
            {
                draggableTile.tileSpawner = this;
            }

            Debug.Log($"Tile parent is: {newTile.transform.parent.name}");
            Debug.Log($"Tile local position is: {newTile.transform.localPosition}");
        }

        // Update the remaining tile count
        UpdateTileCounter();
    }

    // Method to update the text displaying the remaining tile count
    public void UpdateTileCounter()
    {
        if (tileCounterText != null)
        {
            tileCounterText.text = "X" + tileCount.ToString();  // Update the text
        }
        else
        {
            Debug.LogWarning("tileCounterText is not assigned in the inspector.");
        }
    }

    // Method to decrease the tile count
    public void ReduceTileCount()
    {
        if (tileCount > 0)
        {
            tileCount--;
            UpdateTileCounter();
        }
    }
}
