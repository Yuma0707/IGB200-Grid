using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject panelPrefab; // �q�p�l���̃v���n�u
    public GridLayoutGroup gridLayoutGroup; // �e�p�l���ɃA�^�b�`���ꂽGrid Layout Group
    public int gridSize = 10; // �f�t�H���g�̃O���b�h�T�C�Y

    void Start()
    {
        SetGridSize(gridSize); // �����ݒ�
    }

    public void GenerateGrid(int newSize)
    {
        // �����̃p�l�����N���A
        foreach (Transform child in gridLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        // �V�����O���b�h�T�C�Y��ݒ�
        gridSize = newSize;

        // �e�p�l���̕��ƍ������擾
        RectTransform rectTransform = gridLayoutGroup.GetComponent<RectTransform>();
        float panelWidth = rectTransform.rect.width;
        float panelHeight = rectTransform.rect.height;

        // �Z���T�C�Y���v�Z (�؂�グ���g�p)
        float cellSize = Mathf.Min(panelWidth / gridSize, panelHeight / gridSize);

        // Grid Layout Group �̐ݒ�
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);

        // �K�v�ɉ����āA�Z���Ԃɏ����]�T���������邽�߂̃X�y�[�V���O��ǉ�
        gridLayoutGroup.spacing = new Vector2(
            (panelWidth - (cellSize * gridSize)) / (gridSize - 1),
            (panelHeight - (cellSize * gridSize)) / (gridSize - 1)
        );

        // �V�����Z���𐶐�
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
