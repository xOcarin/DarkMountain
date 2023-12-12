using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RescueeAudioScript : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public float delay = 5f;  // Delay in seconds

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine
        StartCoroutine(PlayAudioWithDelay());
    }

    public IEnumerator PlayAudioWithDelay()
    {
        while (true)
        {
            // Generate a random delay between 1 and 6 seconds
            float randomDelay = Random.Range(3f, 10f);

            // Wait for the random delay
            yield return new WaitForSeconds(randomDelay);

            // Play the audio clip
            source.PlayOneShot(clip);
        }
    }
}