using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab; // ��������^�C���̃v���n�u
    public int tileCount = 4; // �c��̃^�C�����������J�E���^
    public TextMeshProUGUI tileCounterText; // �c��^�C������\������TextMeshProUGUI

    private void Start()
    {
        // �c��^�C�������X�V
        UpdateTileCounter();

        // �^�C���𐶐�
        SpawnTiles();
    }

    public void SpawnTiles()
    {
        Debug.Log("SpawnTiles called!");

        // TileCount�Ɋ�Â��ĕ����^�C���𐶐�
        for (int i = 0; i < tileCount; i++)
        {
            GameObject newTile = Instantiate(tilePrefab, transform.position, Quaternion.identity);

            // TileSpawner�̎q�I�u�W�F�N�g�ɐݒ�
            newTile.transform.SetParent(this.transform, false);

            // ���[�J�����W��(0, 0, 0)�Ƀ��Z�b�g
            newTile.transform.localPosition = Vector3.zero;

            // DraggableTile �X�N���v�g��TileSpawner��n��
            DraggableTile draggableTile = newTile.GetComponent<DraggableTile>();
            if (draggableTile != null)
            {
                draggableTile.tileSpawner = this;
            }

            Debug.Log($"Tile parent is: {newTile.transform.parent.name}");
            Debug.Log($"Tile local position is: {newTile.transform.localPosition}");
        }

        // �c��^�C�������X�V
        UpdateTileCounter();
    }

    // �c��^�C������\������e�L�X�g���X�V���郁�\�b�h
    public void UpdateTileCounter()
    {
        if (tileCounterText != null)
        {
            tileCounterText.text = "�~" + tileCount.ToString();  // �e�L�X�g���X�V
        }
        else
        {
            Debug.LogWarning("tileCounterText is not assigned in the inspector.");
        }
    }

    // �^�C���J�E���g�����炷���\�b�h
    public void ReduceTileCount()
    {
        if (tileCount > 0)
        {
            tileCount--;
            UpdateTileCounter();
        }
    }
}
