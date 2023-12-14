using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{
    public static bool hasFlare;
    public static int flaresGiven;

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
            
            if (RenderSettings.fogDensity <= .12f)
            {
                RenderSettings.fogDensity += .001f;
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlarePowerUp") && !hasFlare)
        {
            other.gameObject.SetActive(false);
            hasFlare = true;
        }
    }

}
