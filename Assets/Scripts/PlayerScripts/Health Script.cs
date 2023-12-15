using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public static float playerHealth = 3;
    public static bool invulnerable;
    
    public float knockbackForce;
    public float knockbackDuration;
    public float upwardKnockbackHeight;
    private Vector3 velocity;
    
    
    private CharacterController characterController;
    
    public Canvas canvas;
    
    private Animator characterAnimator;
    public GameObject characterModel;

    public float iFrameDuration;

    public bool restartTriggered;
    
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        canvas = GetComponentInChildren<Canvas>();
        characterAnimator = characterModel.GetComponent<Animator>();
        invulnerable = false;
        //characterAnimator.SetBool("isHurt", false);

    }

    void Update()
    {
        


        if (playerHealth == 2)
        {
            canvas.transform.GetChild(3).gameObject.SetActive(false);
        }
        if (playerHealth == 1)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (RescueeAudioScript.levelOver)
        {
            
        }
        
        
        
        
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bird") && !invulnerable)
        {
            playerAudioHandler.Hurt();
            characterAnimator.SetBool("isHurt", true);
            playerHealth--;
            Debug.Log("Health: " + playerHealth);
            StartCoroutine(StartIFrames());
            Vector3 forceDirection = (transform.position - other.transform.position).normalized;
            Vector3 hitForceVector = forceDirection * knockbackForce;
            StartCoroutine(Knockback(hitForceVector));
            StartCoroutine(StopMovement());

        }
    }
    



    
    
    private IEnumerator StartIFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(iFrameDuration);
        invulnerable = false;
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
            // Calculate the upward movement
            float verticalMovement = upwardKnockbackHeight * Time.deltaTime;

            // Apply the knockback force (without vertical movement)
            characterController.Move(force * Time.deltaTime);

            // Apply the upward movement directly to the character's position
            transform.position += new Vector3(0f, verticalMovement, 0f);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }



    
    
    
}
