using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public float playerHealth;
    public bool invulnerable;
    
    public float knockbackForce;
    
    private CharacterController characterController;
    
    private Vector3 velocity;

    public float knockbackDuration;

    public Canvas canvas;
    
    private Animator characterAnimator;
    public GameObject characterModel;

    public float iFrameDuration;

    
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        canvas = GetComponentInChildren<Canvas>();
        characterAnimator = characterModel.GetComponent<Animator>();
        //characterAnimator.SetBool("isHurt", false);

    }

    void Update()
    {
        
        //restart scene if players health is less than one.
        if(playerHealth < 1){RestartScene();}


        if (playerHealth == 2)
        {
            canvas.transform.GetChild(3).gameObject.SetActive(false);
        }
        if (playerHealth == 1)
        {
            
            canvas.transform.GetChild(2).gameObject.SetActive(false);
        }
        
        
        
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bird") && !invulnerable)
        {
            characterAnimator.SetBool("isHurt", true);
            playerHealth--;
            Debug.Log("Health: " + playerHealth);
            StartCoroutine(StartIFrames());
            StartCoroutine(FlickerModel());
            Vector3 forceDirection = (transform.position - other.transform.position).normalized;
            Vector3 hitForceVector = forceDirection * knockbackForce;
            StartCoroutine(Knockback(hitForceVector));
            StartCoroutine(StopMovement());

        }
    }
    



    private void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    private IEnumerator StartIFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(iFrameDuration);
        invulnerable = false;
        ModelFlickerScript.ChangeBackToOriginalMaterial();
    }
    
    IEnumerator FlickerModel()
    {
        Debug.LogError("FLICKERING!!");
        float timer = 0f;

        while (timer < iFrameDuration)
        {
            ModelFlickerScript.ChangeMaterial();
            yield return new WaitForSeconds(0.5f);
            Debug.LogError("SHOULD BE to original");
            ModelFlickerScript.ChangeBackToOriginalMaterial();
        }
    }
    
    private IEnumerator StopMovement()
    {
        PlayerMovement.canMove = false;
        yield return new WaitForSeconds(.5f);
        PlayerMovement.canMove = true;
        characterAnimator.SetBool("isHurt", false);
    }
    
    IEnumerator Knockback(Vector3 force)
    {
        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            // Move the character gradually
            characterController.Move(force * Time.deltaTime);
            
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    
    
}
