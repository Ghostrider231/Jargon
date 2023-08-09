using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAnimation : MonoBehaviour
{
    
    public float interactRange = 2.0f;
    public LayerMask interactableLayer;
    public Animator animator;
    private bool isInRange = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {



        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange, interactableLayer);

        if (colliders.Length > 0)
        {
            // At least one object in range
            if (!isInRange)
            {
                // Entering range, play animation
                isInRange = true;
                animator.SetBool("IsInRange", true);
            }
        }
        else
        {
            // No objects in range
            if (isInRange)
            {
                // Exiting range, cancel animation
                isInRange = false;
                animator.SetBool("IsInRange", false);
            }
        }
    }
}
