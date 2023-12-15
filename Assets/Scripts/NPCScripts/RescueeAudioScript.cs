using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RescueeAudioScript : MonoBehaviour
{
    public AudioSource source;
    
    
    public AudioSource cry1;
    public AudioSource cry2;
    public AudioSource cry3;
    public AudioSource helicopter;
    public AudioSource thanks;
    
    private Animator npcAnimator;
    public GameObject npcModel;
    public GameObject endScreen;

    public static bool keepTiming = true;
    public static bool levelOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine
        npcAnimator = npcModel.GetComponent<Animator>();
        StartCoroutine(PlayAudioWithDelay());
        levelOver = false;
    }

    public IEnumerator PlayAudioWithDelay()
    {
        while (true)
        {
            // Generate a random delay between 1 and 6 seconds
            float randomDelay = Random.Range(3f, 7f);
            int randomNum = Random.Range(1, 4);

            // Wait for the random delay
            yield return new WaitForSeconds(randomDelay);

            // Play the audio clip
            if (npcAnimator.GetBool("NPCShiver"))
            {
                if (randomNum == 1)
                {
                    cry1.PlayOneShot(cry1.clip, 1.0f);
                }
                else if (randomNum == 2)
                {
                    cry2.PlayOneShot(cry2.clip, 1.0f);
                }
                else if (randomNum == 3)
                {
                    cry3.PlayOneShot(cry3.clip, 1.0f);
                }
            }
            
        }
    }

    private void Update()
    {
        if (npcAnimator.GetBool("NPCIdle") && !helicopter.isPlaying)
        {
            helicopter.Play();
            Invoke("PlayThanksSound", 1f);
        }
        else if((!npcAnimator.GetBool("NPCRescue") && !npcAnimator.GetBool("NPCIdle")) && helicopter.isPlaying)
        {
            helicopter.Stop();
        }
    }


    void EndGame()
    {
        Debug.Log("flares given: " + PlayerItemHandler.flaresGiven );
        if (PlayerItemHandler.flaresGiven == 3)
        {
            keepTiming = false;
            levelOver = true;
            Debug.Log(" YOOOOOO");
            displayEndScreen();
            KillAllEnemies();
            PlayerMovement.dialogue = true;
            PlayerItemHandler.flaresGiven = 4;
        }
    }

    void KillAllEnemies()
    {
        // Find all objects with the specified tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Bird");

        // Destroy each enemy
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
    
    private void displayEndScreen()
    {
       endScreen.SetActive(true);
    }

    private void PlayThanksSound()
    {
        thanks.PlayOneShot(thanks.clip, 1.0f);
        EndGame();
    }
    
    
    
}