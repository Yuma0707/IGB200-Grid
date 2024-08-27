using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGridManager : MonoBehaviour
{
    public GameObject panelPrefab; // 子パネルのプレハブ
    public RectTransform parentPanel; // 親パネル
    public int gridSize = 10; // デフォルトのグリッドサイズ

    void Start()
    {
        GenerateGrid(gridSize);
    }

    public void GenerateGrid(int newSize)
    {
        // 既存のパネルをクリア
        foreach (Transform child in parentPanel)
        {
            Destroy(child.gameObject);
        }

        // 新しいグリッドサイズを設定
        gridSize = newSize;

        // 親パネルの幅と高さを取得
        float panelWidth = parentPanel.rect.width;
        float panelHeight = parentPanel.rect.height;

        // セルサイズを計算
        float cellSize = Mathf.Min(panelWidth / gridSize, panelHeight / gridSize);

        // グリッド全体を中央に揃えるためのオフセットを計算
        float offsetX = (panelWidth - (cellSize * gridSize)) / 2;
        float offsetY = (panelHeight - (cellSize * gridSize)) / 2;

        // セルを手動で配置
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                GameObject panel = Instantiate(panelPrefab, parentPanel);
                panel.name = $"Panel_{row}_{col}";

                RectTransform panelRect = panel.GetComponent<RectTransform>();
                panelRect.sizeDelta = new Vector2(cellSize, cellSize);
                panelRect.anchorMin = new Vector2(0, 1);
                panelRect.anchorMax = new Vector2(0, 1);
                panelRect.pivot = new Vector2(0, 1);

                // パネルの位置を計算して設定
                float xPos = col * cellSize + offsetX;
                float yPos = -row * cellSize - offsetY;
                panelRect.anchoredPosition = new Vector2(xPos, yPos);
            }
        }
    }

    public void SetGridSize(int newSize)
    {
        GenerateGrid(newSize);
    }
}
