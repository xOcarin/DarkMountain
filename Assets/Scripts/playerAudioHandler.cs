using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudioHandler : MonoBehaviour

{
    
    public AudioSource audioSource1;
    public static AudioSource audioSource2;

    public static bool hasPlayed;
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        
        if (audioSources.Length >= 2)
        {
            audioSource2 = audioSources[1];
        }
    }
    
    void Update()
    {
        //Debug.Log(PlayerMovement.isJumping);
        if (PlayerMovement.isWalking && !audioSource1.isPlaying && !PlayerMovement.isJumping)
        {
            audioSource1.Play();
        }
        else if (!PlayerMovement.isWalking || PlayerMovement.isJumping)
        {
            audioSource1.Stop();
        }
    }

    public static void PlayNotif()
    {
        if (!hasPlayed)
        {
            audioSource2.PlayOneShot(audioSource2.clip, 1.0f);
            hasPlayed = true;
        }
    }
}
