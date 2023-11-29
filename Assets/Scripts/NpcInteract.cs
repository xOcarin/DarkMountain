using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    //public GameObject d_template;
    //public GameObject canvas;
    public void Interact()
    {
        Debug.Log("next to npc!");
        //PlayerMovement.dialogue = true;
        //transform.GetChild(0).gameObject.SetActive(true);
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
