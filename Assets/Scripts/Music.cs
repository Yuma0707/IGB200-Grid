using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    public Button toggleButton;
    private bool isPlaying = true;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleMusic);
    }

    void ToggleMusic()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            audioSource.Play();
            isPlaying = true;
        }
    }
}
