using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    // Public variables to configure in the inspector
    public GameObject[] tilePrefabs; // Change to array to handle multiple tiles.配列に変更して複数のタイルを扱えるようにします
    public int tileCount; // Set the number of tiles to spawn

    private void Start()
    {
        SpawnTile();
    }

    public void SpawnTile()
    {
        Debug.Log("SpawnTile called!");

        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            // Generate Tile.タイルを生成
            GameObject newTile = Instantiate(tilePrefabs[i], transform.position, Quaternion.identity);

            // Set as a child object of TileSpawner.TileSpawner の子オブジェクトに設定
            newTile.transform.SetParent(this.transform, false);

            // Reset local coordinates to (0, 0, 0).ローカル座標を (0, 0, 0) にリセット
            newTile.transform.localPosition = Vector3.zero;

            Debug.Log($"Tile parent is: {newTile.transform.parent.name}");
            Debug.Log($"Tile local position is: {newTile.transform.localPosition}");
        }
    }
}
