using System.Collections;
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
            // Wait for the specified delay
            yield return new WaitForSeconds(delay);

            // Play the audio clip
            source.PlayOneShot(clip);
        }
    }
}