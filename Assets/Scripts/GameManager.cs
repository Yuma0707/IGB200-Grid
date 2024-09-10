using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string clearKey = "GameCleared"; // Saved Data Key.セーブデータのキー

    // Called when the game is cleared.ゲームがクリアされた場合に呼び出される
    public void SaveGameClear()
    {
        // Save clear flag (1 = cleared).クリアフラグをセーブ (1 = クリア済み)
        PlayerPrefs.SetInt(clearKey, 1);
        PlayerPrefs.Save();
        Debug.Log("Game cleared and saved!");
    }

    // Load saved data.セーブデータを読み込む
    public bool IsGameCleared()
    {
        // Get clear flag (1 means cleared).クリアフラグを取得 (1ならクリア済み)
        return PlayerPrefs.GetInt(clearKey, 0) == 1;
    }

    // Reset saved data for debugging.デバッグ用にセーブデータをリセットする
    public void ResetSaveData()
    {
        PlayerPrefs.DeleteKey(clearKey);
        PlayerPrefs.Save();
        Debug.Log("Save data reset.");
    }
}
