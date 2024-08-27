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
        // Clear existing panels.�����̃p�l�����N���A
        foreach (Transform child in parentPanel)
        {
            Destroy(child.gameObject);
        }

        // Set new grid size.�V�����O���b�h�T�C�Y��ݒ�
        gridSize = newSize;

        // Get width and height of parent panel.�e�p�l���̕��ƍ������擾
        float panelWidth = parentPanel.rect.width;
        float panelHeight = parentPanel.rect.height;

        // Calculate cell size.�Z���T�C�Y���v�Z
        float cellSize = Mathf.Min(panelWidth / gridSize, panelHeight / gridSize);

        // Calculate offset to center the entire grid.�O���b�h�S�̂𒆉��ɑ����邽�߂̃I�t�Z�b�g���v�Z
        float offsetX = (panelWidth - (cellSize * gridSize)) / 2;
        float offsetY = (panelHeight - (cellSize * gridSize)) / 2;

        // Place cells manually.�Z�����蓮�Ŕz�u
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

                // Calculate and set the panel position.�p�l���̈ʒu���v�Z���Đݒ�
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
