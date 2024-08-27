using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject panelPrefab; // 子パネルのプレハブ
    public GridLayoutGroup gridLayoutGroup; // 親パネルにアタッチされたGrid Layout Group
    public int gridSize = 10; // デフォルトのグリッドサイズ

    void Start()
    {
        SetGridSize(gridSize); // 初期設定
    }

    public void GenerateGrid(int newSize)
    {
        // 既存のパネルをクリア
        foreach (Transform child in gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // 新しいグリッドサイズを設定
        gridSize = newSize;

        // 親パネルの幅と高さを取得
        RectTransform rectTransform = gridLayoutGroup.GetComponent<RectTransform>();
        float panelWidth = rectTransform.rect.width;
        float panelHeight = rectTransform.rect.height;

        // セルサイズを計算 (切り上げを使用)
        float cellSize = Mathf.Min(panelWidth / gridSize, panelHeight / gridSize);

        // Grid Layout Group の設定
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);

        // 必要に応じて、セル間に少し余裕を持たせるためのスペーシングを追加
        gridLayoutGroup.spacing = new Vector2(
            (panelWidth - (cellSize * gridSize)) / (gridSize - 1),
            (panelHeight - (cellSize * gridSize)) / (gridSize - 1)
        );

        // 新しいセルを生成
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            GameObject panel = Instantiate(panelPrefab, gridLayoutGroup.transform);
            panel.name = "Panel_" + i;
        }
    }


    public void SetGridSize(int newSize)
    {
        GenerateGrid(newSize);
    }
}
