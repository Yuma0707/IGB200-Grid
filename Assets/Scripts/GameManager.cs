using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string clearKey = "GameCleared"; // Saved Data Key.�Z�[�u�f�[�^�̃L�[

    // Called when the game is cleared.�Q�[�����N���A���ꂽ�ꍇ�ɌĂяo�����
    public void SaveGameClear()
    {
        // Save clear flag (1 = cleared).�N���A�t���O���Z�[�u (1 = �N���A�ς�)
        PlayerPrefs.SetInt(clearKey, 1);
        PlayerPrefs.Save();
        Debug.Log("Game cleared and saved!");
    }

    // Load saved data.�Z�[�u�f�[�^��ǂݍ���
    public bool IsGameCleared()
    {
        // Get clear flag (1 means cleared).�N���A�t���O���擾 (1�Ȃ�N���A�ς�)
        return PlayerPrefs.GetInt(clearKey, 0) == 1;
    }

    // Reset saved data for debugging.�f�o�b�O�p�ɃZ�[�u�f�[�^�����Z�b�g����
    public void ResetSaveData()
    {
        PlayerPrefs.DeleteKey(clearKey);
        PlayerPrefs.Save();
        Debug.Log("Save data reset.");
    }
}
