using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelselect : MonoBehaviour
{
    public GameObject level1star;
    public GameObject level2star;
    public GameObject level3star;

    void Start()
    {
        if (PlayerPrefs.GetInt("Level1Completed", 0) == 1)
        {
            level1star.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level2Completed", 0) == 1)
        {
            level2star.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level3Completed", 0) == 1)
        {
            level3star.SetActive(true);
        }

    }
}
