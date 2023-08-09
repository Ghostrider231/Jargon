using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    float interactRange = 2f;




    void Start()
    {
        
    }


    void Update()
    {
        //this portion of our code helps to determin what is in the player's range not just the NPC
        //however this allows the player to use the interact button in general
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray)
            {
               Debug.Log("E was press and " + collider + " was found\n");
               if (collider.TryGetComponent(out NPCinteraction npcInteraction))
                {
                    npcInteraction.Interact();
                }
            }
        }
    }
}
