using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    private static Animator canvasAnimator;

    private void Start()
    {
        canvasAnimator = GetComponent<Animator>();
        canvasAnimator.SetBool("isDead", false);
    }

    private void Update()
    {
        Debug.Log("d script health: " + HealthScript.playerHealth);
        if(HealthScript.playerHealth < 1)
        {
            canvasAnimator.Play("DeathScreen");
            StartCoroutine(resetScene());
        }

    }


    private IEnumerator resetScene()
    {
        yield return new WaitForSeconds(3f);
        HealthScript.playerHealth = 3;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}