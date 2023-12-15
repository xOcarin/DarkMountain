using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gathererScript : MonoBehaviour
{
    [SerializeField]
    private GameObject face1;

    [SerializeField]
    private GameObject face2;

    [SerializeField]
    private GameObject face3;
    
    
    
    public AudioSource got1;
    public AudioSource got2;
    public AudioSource got3;
    
    public AudioSource Victory;
    
    private bool clipPlayed = false;
    private bool clip2Played = false;
    private bool clip3Played = false;
    
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int flares = PlayerItemHandler.flaresGiven;

        if (flares == 1 && !clipPlayed)
        {
            face1.SetActive(true);
            got1.PlayOneShot(got1.clip, 0.10f);
            clipPlayed = true;
        }
        else if (flares == 2 && !clip2Played)
        {
            face2.SetActive(true);
            got2.PlayOneShot(got2.clip, 0.10f);
            clip2Played = true;
        }
        else if (flares == 3 && !clip3Played)
        {
            face3.SetActive(true);
            got3.PlayOneShot(got3.clip, 0.10f);
            clip3Played = true;
        }
    }
    
    void TransitionToNewScene()
    {
        // Replace "YourSceneName" with the name of the scene you want to transition to
        SceneManager.LoadScene("YourSceneName");
    }
    
    
}
