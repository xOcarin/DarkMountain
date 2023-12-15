using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class playerAudioHandler : MonoBehaviour

{
    
    public AudioSource audioSource1;
    public static AudioSource audioSource2;
    
    
    public static AudioSource jump;
    public static AudioSource hurt;
    public static AudioSource music;

    public static bool hasPlayed;

    public static int time;
    
    
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        
        music.Play();
        if (audioSources.Length >= 2)
        {
            audioSource2 = audioSources[1];
            jump = audioSources[2];
            hurt = audioSources[3];
        }
        StartCoroutine(StartTimer());
    }
    
    void Update()
    {
        
        //Debug.Log(PlayerMovement.isJumping);
        if (PlayerMovement.isWalking && !audioSource1.isPlaying && !PlayerMovement.isJumping && PlayerItemHandler.flaresGiven < 3)
        {
            audioSource1.Play();
        }
        else if (!PlayerMovement.isWalking || PlayerMovement.isJumping)
        {
            audioSource1.Stop();
        }

        /*if (PlayerMovement.isOnGround)
        {
            if (Input.GetButton("Jump"))
            {
                jump.PlayOneShot(jump.clip);
            }
        }*/
        
    }

    private IEnumerator StartTimer()
    {
        while (RescueeAudioScript.keepTiming)
        {
            time++;
            yield return new WaitForSeconds(1f);
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

    public static void Hurt()
    {
        hurt.PlayOneShot(hurt.clip, 1.0f);
    }
    
    public static void ItemGetSound()
    {
        jump.PlayOneShot(jump.clip);
    }
    
    
    
    
    
    
    
    
}
