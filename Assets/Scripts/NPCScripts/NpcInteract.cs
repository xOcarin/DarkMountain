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



    void Start()
    {
        npcAnimator = npcModel.GetComponent<Animator>();

    }
    public void Interact()
    {
        Debug.Log("next to npc!");
        isFound = true;
        if (PlayerItemHandler.hasFlare == true)
        {
            PlayerItemHandler.flaresGiven++;
            PlayerItemHandler.hasFlare = false;
            transform.LookAt(player);
            //StartCoroutine(SetDialoguePause());
            StartCoroutine(PlayRescueAnimation());
        }
        else
        {
            //play error sound
        }
        
        
        
    }

    private IEnumerator PlayRescueAnimation()
    {
        Debug.Log("HIT");
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
