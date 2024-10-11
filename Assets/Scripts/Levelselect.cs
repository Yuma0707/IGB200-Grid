using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelselect : MonoBehaviour
{
    public GameObject star;

    void Start()
    {
        if (PlayerPrefs.GetInt("Level1Completed", 0) == 1)
        {
            star.SetActive(true);
        }
        else
        {
            star.SetActive(false);
        }
    }
}
