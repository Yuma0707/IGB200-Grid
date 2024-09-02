using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // このメソッドはボタンにアタッチされます
    public void LoadLevel1()
    {
        // シーンをロードする
        SceneManager.LoadScene("Level1");
    }
}

