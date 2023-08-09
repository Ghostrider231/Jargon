using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVEMENT : MonoBehaviour
{

    Animator Animator; //for animation
    SpriteRenderer Ssprite; //for animation

    public float speed = 1; //for movement
    private Rigidbody rigidBody; //for movement
    private float V = 10.0f; //for movement
    private float horizontalInput; //for movement
    private float verticalInput; //for movement
    private Vector3 movedirection; //for movement

    public Vector3 jump; //for Jump
    public float jumpForce = 2.0f; //for Jump
    public bool isGrounded; //for Jump
    


    void Start()
    {
        Animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        Ssprite = GetComponent<SpriteRenderer>();

        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    //function used for jumping  (currently, could be used for more)
    void OnCollisionStay()
    {
        isGrounded = true;
        Animator.SetBool("is Grounded", true);
    }

    void Update()
    {

        /* Movement Portion of code */

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        movedirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movedirection * V * Time.deltaTime);



        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Animator.SetBool("Is moving", true);
            if (Input.GetKey(KeyCode.D))
            {
                Ssprite.flipX = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                Ssprite.flipX = false;
            }
        }
        else
        {
            Animator.SetBool("Is moving", false);
        }

        /* END of Movement Portion of code */


        /* Jump portion of code */



        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rigidBody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Animator.SetBool("is Grounded", false);

        }

        if (rigidBody.velocity.y > 0.1f)
        {
            Animator.SetBool("is jumping", true);
            Animator.SetBool("is falling", false);
        }
        else if (rigidBody.velocity.y < -0.1f)
        {
            Animator.SetBool("is jumping", false);
            Animator.SetBool("is falling", true);
        }
        else
        {
            Animator.SetBool("is jumping", false);
            Animator.SetBool("is falling", false);
        }

        /* END of Jump portion of code */
    }
}