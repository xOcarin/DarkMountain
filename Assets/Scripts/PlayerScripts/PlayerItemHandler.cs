using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{
    public static bool hasFlare;
    public static int flaresGiven = 0;

    private GameObject flare;

    void Start()
    {
        flare = GameObject.FindWithTag("PlayerFlare");
        flare.SetActive(true);
    }
    
    void Update()
    {
        if (hasFlare)
        {
            flare.SetActive(true);
            if (RenderSettings.fogDensity >= .06f)
            {
                RenderSettings.fogDensity -= .001f;
            }


        }
        else
        {
            flare.SetActive(false);
            
            if (RenderSettings.fogDensity <= .18f)
            {
                RenderSettings.fogDensity += .001f;
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlarePowerUp") && !hasFlare)
        {
            playerAudioHandler.ItemGetSound();
            other.gameObject.SetActive(false);
            hasFlare = true;
        }
    }

}
