using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private HashSet<Collider> previousColliders = new HashSet<Collider>();
    public float interactRange = 4f;
    public float deactivationThreshold = 0.5f;

    public static bool InRange;

    private void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        // Deactivate visuals for NPCs that were in the previous frame but not in the current frame
        foreach (Collider prevCollider in previousColliders)
        {
            if (!colliderArray.Contains(prevCollider))
            {
                float distance = Vector3.Distance(transform.position, prevCollider.transform.position);
                if (distance > interactRange + deactivationThreshold)
                {
                    // Check if the collider has a child before trying to access it
                    if (prevCollider.transform.childCount > 0)
                    {
                        prevCollider.transform.GetChild(0).gameObject.SetActive(false);
                        InRange = false;
                        playerAudioHandler.hasPlayed = false;
                    }
                }
            }
        }

        // Update the set of current colliders for the next frame
        previousColliders.Clear();
        foreach (Collider collider in colliderArray)
        {
            
            previousColliders.Add(collider);

            if (collider.TryGetComponent(out NpcInteract npcInteract))
            {
                // Activate visuals for NPCs in the current frame
                if (collider.transform.childCount > 0)
                {
                    InRange = true;
                    collider.transform.GetChild(0).gameObject.SetActive(true);
                    playerAudioHandler.PlayNotif();
                }
                
                
                

                // Interact with NPCs if the "Sprint" button is pressed
                if (Input.GetButton("Sprint"))
                {
                    npcInteract.Interact();
                }
            }
        }
    }
}