using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSoundController : MonoBehaviour
{
    public AudioClip pickUpSound;   
    public AudioClip putDownSound;  
    private AudioSource audioSource;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }


    public void PlayPickUpSound()
    {
        if (pickUpSound != null)
        {
            audioSource.clip = pickUpSound;
            audioSource.Play();
        }
    }

  
    public void OnEndDrag()
    {
        if (putDownSound != null)
        {
            audioSource.clip = putDownSound;
            audioSource.Play();
        }
    }
}
