using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    private Animator canvasAnimator;
    public AudioSource music;

    private void Start()
    {
        canvasAnimator = GetComponent<Animator>();
        music.Play();
    }

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        canvasAnimator.SetBool("buttonPressed", true);
        Invoke("NextOne", 1f);
    }

    private void NextOne()
    {
        music.Stop();
        SceneManager.LoadScene("SampleScene");
    }
}
