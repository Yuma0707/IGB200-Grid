using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;      // Slider for volume adjustment
    private AudioSource audioSource; // Reference to AudioSource to play background music and other sounds

    void Start()
    {
        // Automatically locate AudioSource in the scene
        audioSource = FindObjectOfType<AudioSource>();

        if (audioSource != null && volumeSlider != null)
        {
            // Set slider to current volume of AudioSource
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        else
        {
            Debug.LogWarning("AudioSource or VolumeSlider is missing.");
        }
    }

    // Methods for setting volume
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
