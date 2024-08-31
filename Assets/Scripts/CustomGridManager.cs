using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGridManager : MonoBehaviour
{
    public GameObject panelPrefab; // Prefabricated child panel.�q�p�l���̃v���n�u
    public RectTransform parentPanel; // main panel.�e�p�l��
    public int gridSize = 10; // Default grid size.�f�t�H���g�̃O���b�h�T�C�Y

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

                // Set anchor and pivot to center.�A���J�[�ƃs�{�b�g�𒆉��ɐݒ�
                panelRect.anchorMin = new Vector2(0.5f, 0.5f);
                panelRect.anchorMax = new Vector2(0.5f, 0.5f);
                panelRect.pivot = new Vector2(0.5f, 0.5f);

                // Corrected calculation of anchoredPosition.anchoredPosition�̌v�Z���C��
                float xPos = (col - gridSize / 2f + 0.5f) * cellSize;
                float yPos = -(row - gridSize / 2f + 0.5f) * cellSize;
                panelRect.anchoredPosition = new Vector2(xPos, yPos);
            }
        }
    }

    public void SetGridSize(int newSize)
    {
        GenerateGrid(newSize);
    }
}