using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    // Public variables to configure in the inspector
    public GameObject[] tilePrefabs; // Change to array to handle multiple tiles.�z��ɕύX���ĕ����̃^�C����������悤�ɂ��܂�
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
            // Generate Tile.�^�C���𐶐�
            GameObject newTile = Instantiate(tilePrefabs[i], transform.position, Quaternion.identity);

            // Set as a child object of TileSpawner.TileSpawner �̎q�I�u�W�F�N�g�ɐݒ�
            newTile.transform.SetParent(this.transform, false);

            // Reset local coordinates to (0, 0, 0).���[�J�����W�� (0, 0, 0) �Ƀ��Z�b�g
            newTile.transform.localPosition = Vector3.zero;

            Debug.Log($"Tile parent is: {newTile.transform.parent.name}");
            Debug.Log($"Tile local position is: {newTile.transform.localPosition}");
        }
    }
}
