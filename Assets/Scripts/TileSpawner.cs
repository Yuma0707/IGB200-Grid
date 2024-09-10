using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab; // 生成するタイルのプレハブ
    public int tileCount = 4; // 残りのタイル数を示すカウンタ
    public TextMeshProUGUI tileCounterText; // 残りタイル数を表示するTextMeshProUGUI

    private void Start()
    {
        // 残りタイル数を更新
        UpdateTileCounter();

        // タイルを生成
        SpawnTiles();
    }

    public void SpawnTiles()
    {
        Debug.Log("SpawnTiles called!");

        // TileCountに基づいて複数タイルを生成
        for (int i = 0; i < tileCount; i++)
        {
            GameObject newTile = Instantiate(tilePrefab, transform.position, Quaternion.identity);

            // TileSpawnerの子オブジェクトに設定
            newTile.transform.SetParent(this.transform, false);

            // ローカル座標を(0, 0, 0)にリセット
            newTile.transform.localPosition = Vector3.zero;

            // DraggableTile スクリプトにTileSpawnerを渡す
            DraggableTile draggableTile = newTile.GetComponent<DraggableTile>();
            if (draggableTile != null)
            {
                draggableTile.tileSpawner = this;
            }

            Debug.Log($"Tile parent is: {newTile.transform.parent.name}");
            Debug.Log($"Tile local position is: {newTile.transform.localPosition}");
        }

        // 残りタイル数を更新
        UpdateTileCounter();
    }

    // 残りタイル数を表示するテキストを更新するメソッド
    public void UpdateTileCounter()
    {
        if (tileCounterText != null)
        {
            tileCounterText.text = "×" + tileCount.ToString();  // テキストを更新
        }
        else
        {
            Debug.LogWarning("tileCounterText is not assigned in the inspector.");
        }
    }

    // タイルカウントを減らすメソッド
    public void ReduceTileCount()
    {
        if (tileCount > 0)
        {
            tileCount--;
            UpdateTileCounter();
        }
    }
}
