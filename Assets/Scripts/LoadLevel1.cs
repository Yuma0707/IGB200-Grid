using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Changed to a method that takes a scene name as an argument.�����Ƃ��ăV�[�������󂯎�郁�\�b�h�ɕύX
    public void LoadScene(string sceneName)
    {
        // Load a Scene.�V�[�������[�h����
        SceneManager.LoadScene(sceneName);
    }
}


