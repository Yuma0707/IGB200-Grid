using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGridManager : MonoBehaviour
{
    public GameObject panelPrefab; // Prefabricated child panel.子パネルのプレハブ
    public GameObject gridScoreIndicatorPrefab; // Add this for the new prefab
    public RectTransform parentPanel; // main panel.親パネル
    public int gridSize = 10; // Default grid size.デフォルトのグリッドサイズ

    void Start()
    {
        GenerateGrid(gridSize);
    }

    public void GenerateGrid(int newSize)
    {
        // Clear existing panels
        foreach (Transform child in parentPanel)
        {
            Destroy(child.gameObject);
        }

        gridSize = newSize;
        float panelWidth = parentPanel.rect.width;
        float panelHeight = parentPanel.rect.height;
        float cellSize = Mathf.Min(panelWidth / gridSize, panelHeight / gridSize);
        float offsetX = (panelWidth - (cellSize * gridSize)) / 2;
        float offsetY = (panelHeight - (cellSize * gridSize)) / 2;

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                GameObject panel = Instantiate(panelPrefab, parentPanel);
                panel.name = $"Panel_{row}_{col}";

                RectTransform panelRect = panel.GetComponent<RectTransform>();
                panelRect.sizeDelta = new Vector2(cellSize, cellSize);

                // Set anchor and pivot to center.アンカーとピボットを中央に設定
                panelRect.anchorMin = new Vector2(0.5f, 0.5f);
                panelRect.anchorMax = new Vector2(0.5f, 0.5f);
                panelRect.pivot = new Vector2(0.5f, 0.5f);

                // Corrected calculation of anchoredPosition.anchoredPositionの計算を修正
                float xPos = (col - gridSize / 2f + 0.5f) * cellSize;
                float yPos = -(row - gridSize / 2f + 0.5f) * cellSize;
                panelRect.anchoredPosition = new Vector2(xPos, yPos);

                // Instantiate and position GridScoreIndicators
                if (row < gridSize - 1)
                {
                    InstantiateGridScoreIndicator(row, col, cellSize, offsetX, offsetY, 0, cellSize / 2); // Below
                }
                if (col < gridSize - 1)
                {
                    InstantiateGridScoreIndicator(row, col, cellSize, offsetX, offsetY, cellSize / 2, 0); // Right
                }
            }
        }
    }

    private void InstantiateGridScoreIndicator(int row, int col, float cellSize, float offsetX, float offsetY, float additionalX, float additionalY)
    {
        GameObject indicator = Instantiate(gridScoreIndicatorPrefab, parentPanel);
        RectTransform indicatorRect = indicator.GetComponent<RectTransform>();
        indicatorRect.sizeDelta = new Vector2(cellSize / 2.5f, cellSize / 2.5f); // Change these numbers to change the scaling relative to the panels size
        indicatorRect.anchorMin = new Vector2(0.5f, 0.5f);
        indicatorRect.anchorMax = new Vector2(0.5f, 0.5f);
        indicatorRect.pivot = new Vector2(0.5f, 0.5f);

        float xPos = (col - gridSize / 2f + 0.5f) * cellSize + additionalX;
        float yPos = -(row - gridSize / 2f + 0.5f) * cellSize - additionalY;
        indicatorRect.anchoredPosition = new Vector2(xPos, yPos);
    }

    public void SetGridSize(int newSize)
    {
        GenerateGrid(newSize);
    }

    public float GetCellSize()
    {
        float panelWidth = parentPanel.rect.width;
        float panelHeight = parentPanel.rect.height;
        return Mathf.Min(panelWidth / gridSize, panelHeight / gridSize);
    }

}
