using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DezMovement : MonoBehaviour
{
    public float speed = 8.5f; // Customizable speed

    private bool isMovementEnabled = true; // Flag to control player movement
    private bool isAnimationEnabled = true; // Flag to control player animation

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody rigidBody;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Vector3 jump;
    private bool isGrounded;

    public float jumpForce = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody>();

        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        animator.SetBool("isGrounded", true);
    }

    void Update()
    {

        if (!isMovementEnabled)
            return; // Exit the function and do not process movement if movement is disabled

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection.magnitude > 0)
        {
            animator.SetBool("IsMoving", isAnimationEnabled); // Only set the animation parameter if animation is enabled
            spriteRenderer.flipX = (horizontalInput > 0);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }

        if (rigidBody.velocity.y > 0.1f)
        {
            animator.SetBool("isJumping", isAnimationEnabled); // Only set the animation parameter if animation is enabled
            animator.SetBool("isFalling", false);
        }
        else if (rigidBody.velocity.y < -0.1f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", isAnimationEnabled); // Only set the animation parameter if animation is enabled
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    private void FixedUpdate()
    {
        if (!isMovementEnabled)
            return; // Exit the function and do not process movement if movement is disabled

        rigidBody.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }

    public void StopAnimation()
    {
        animator.StopPlayback();
    }




    public void EnableAnimation()
    {
        isAnimationEnabled = true;
    }

    public void DisableAnimation()
    {
        isAnimationEnabled = false;
    }
}
