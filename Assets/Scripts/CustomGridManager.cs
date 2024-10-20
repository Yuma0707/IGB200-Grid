using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGridManager : MonoBehaviour
{
    public GameObject panelPrefab;
    public GameObject gridScoreIndicatorPrefab;
    public RectTransform parentPanel;
    public int gridSize = 10;
    public string excludedCoordinates = "";

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

        // Process excludedCoordinates string into a list of coordinate pairs
        List<Vector2Int> excludedCoords = new List<Vector2Int>();
        string[] coords = excludedCoordinates.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string coord in coords)
        {
            if (coord.Length == 2 && int.TryParse(coord[0].ToString(), out int row) && int.TryParse(coord[1].ToString(), out int col))
            {
                excludedCoords.Add(new Vector2Int(row, col));
            }
            else
            {
                Debug.LogWarning($"Invalid coordinate format: {coord}. Coordinates should be in 'rowcol' format (e.g., '00', '12').");
            }
        }

        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                // Check if the current coordinate is in the exclusion list
                if (excludedCoords.Contains(new Vector2Int(row, col)))
                {
                    continue; // Skip this coordinate
                }

                GameObject panel = Instantiate(panelPrefab, parentPanel);
                panel.name = $"Panel_{row}_{col}";

                RectTransform panelRect = panel.GetComponent<RectTransform>();
                panelRect.sizeDelta = new Vector2(cellSize, cellSize);

                panelRect.anchorMin = new Vector2(0.5f, 0.5f);
                panelRect.anchorMax = new Vector2(0.5f, 0.5f);
                panelRect.pivot = new Vector2(0.5f, 0.5f);

                float xPos = (col - gridSize / 2f + 0.5f) * cellSize;
                float yPos = -(row - gridSize / 2f + 0.5f) * cellSize;
                panelRect.anchoredPosition = new Vector2(xPos, yPos);

                // Instantiate and position GridScoreIndicators
                if (row < gridSize - 1 && !excludedCoords.Contains(new Vector2Int(row + 1, col)))
                {
                    InstantiateGridScoreIndicator(row, col, cellSize, offsetX, offsetY, 0, cellSize / 2); // Below
                }
                if (col < gridSize - 1 && !excludedCoords.Contains(new Vector2Int(row, col + 1)))
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
        indicatorRect.sizeDelta = new Vector2(cellSize / 4f, cellSize / 4f);
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