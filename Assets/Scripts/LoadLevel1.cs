using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Changed to a method that takes a scene name as an argument.引数としてシーン名を受け取るメソッドに変更
    public void LoadScene(string sceneName)
    {
        // Load a Scene.シーンをロードする
        SceneManager.LoadScene(sceneName);
    }
}


