using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // ���̃��\�b�h�̓{�^���ɃA�^�b�`����܂�
    public void LoadLevel1()
    {
        // �V�[�������[�h����
        SceneManager.LoadScene("Level1");
    }
}

