using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    //public GameObject d_template;
    //public GameObject canvas;

    public bool isFound;
    public bool localDialogue;
    public Transform player;
    
    private Animator npcAnimator;
    public GameObject npcModel;
    
    private GameObject notif;
    
    [SerializeField]
    private GameObject flareText;
    
    
    
    
    public AudioSource noFlareNoise;
    
    

    void Start()
    {
        npcAnimator = npcModel.GetComponent<Animator>();
    }
    
    
    
    public void Interact()
    {
        Debug.Log(PlayerInteract.isFound);
        if (PlayerItemHandler.hasFlare == true)
        {
            
            PlayerItemHandler.flaresGiven++;
            PlayerItemHandler.hasFlare = false;
            transform.LookAt(player);
            Vector3 newRotation = transform.eulerAngles;
            newRotation.z = 0f;
            newRotation.x = 0f;
            transform.eulerAngles = newRotation;
            StartCoroutine(SetInteractTimeout());
            
            //StartCoroutine(SetDialoguePause());
            StartCoroutine(PlayRescueAnimation());
        }
        else
        {
            noFlareNoise.PlayOneShot(noFlareNoise.clip, 1.0f);
            StartCoroutine(NoFlarePopUp());
        }
        
        
        
    }

    private IEnumerator NoFlarePopUp()
    {
        flareText.SetActive(true);
        yield return new WaitForSeconds(1f);
        flareText.SetActive(false);
    }

    private IEnumerator SetInteractTimeout()
    {
        PlayerInteract.isFound = true;
        Debug.Log("TRUE!!!!");
        yield return new WaitForSeconds(3.2f);
        PlayerInteract.isFound = false;
    }

    private IEnumerator PlayRescueAnimation()
    {
        npcAnimator.SetBool("NPCShiver", false);
        npcAnimator.SetBool("NPCIdle", true);
        yield return new WaitForSeconds(2f);
        
        npcAnimator.SetBool("NPCRescue", true);
        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);

    }
    
    public IEnumerator SetDialoguePause()
    {
        
            PlayerMovement.dialogue = true;
            yield return new WaitForSeconds(2f);
            PlayerMovement.dialogue = false;
            
    }

    void NewDialogue(string text)
    {
        /*
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canvas.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshPro>().text = text;
        */
    }

  
    
}
