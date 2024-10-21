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
        StartCoroutine(InitializeAudioSource());
    }

    IEnumerator InitializeAudioSource()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject audioObject = GameObject.FindWithTag("BGM");
        if (audioObject != null)
        {
            audioSource = audioObject.GetComponent<AudioSource>();
        }

        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.RemoveAllListeners();
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
