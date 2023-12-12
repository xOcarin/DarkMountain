using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoldAudioHandler : MonoBehaviour
{
    public AudioSource audioSource1;
    public static AudioSource audioSource2;

    public static bool hasPlayed;
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        
        if (audioSources.Length >= 2)
        {
            
            audioSource1 = audioSources[0];
            audioSource2 = audioSources[1];
        }
    }
    
    void Update()
    {
        //Debug.Log(PlayerMovement.isJumping);
        if (!EnemyAI.isAttacking && !audioSource1.isPlaying)
        {
            Debug.Log("WALKING NOISE!!!!!!");
            audioSource1.Play();
        }
        else if (EnemyAI.isAttacking)
        {
            audioSource1.Stop();
        }
    }

    public static void PlayAttackNoise()
    {
       
            audioSource2.PlayOneShot(audioSource2.clip, 1.0f);
        
    }
}
