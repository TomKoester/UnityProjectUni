using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class Playermovement : MonoBehaviour
{
    private const float gravity = -9.81f;
    private CharacterController characterController;
    private float yvelocity;
    private AudioSource audioSource;
    private Animator animator;
    private MovingPlatform MP;

    public float groundCastRange;
    public float jumpHeight = 3f;
    public float playerSpeed = 5.0f;
    public AudioClip[] clipFallen;
    public bool isAlive;
    

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
        isAlive = true;
    }

    void FixedUpdate()
    {
        bool isGrounded = characterController.isGrounded;
        if (isGrounded && yvelocity < 0)
        {
            yvelocity = 0f;
        }
        if (!isGrounded)
        {
            MP = null;
        }

        Vector3 move = Camera.main.transform.forward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");
        move.y = 0;
        move.Normalize();
        if(isAlive)
        {
            characterController.Move(move * Time.deltaTime * playerSpeed);
        }
        

        if (move != Vector3.zero && isAlive)
        {
            
            gameObject.transform.forward = move;
        }
        if(isGrounded)
        {
            animator.speed = 1f;
            if (move != Vector3.zero)
            {
                animator.SetBool("RUN", true);
                animator.SetBool("DANCE", false);
            }
            else
            {

                animator.SetBool("DANCE", true);
                animator.SetBool("RUN", false);
                animator.SetBool("JUMP", false);

            }
        }
        // Changes the height position of the player..
        if (isGrounded)
        {
            if (MP)
            {
                characterController.Move(-MP.diff);
            }
            if (Input.GetButton("Jump") && isAlive)
            {
                animator.speed = 0.7f; 
                animator.SetBool("JUMP", true);
                animator.SetBool("DANCE", false);
                animator.SetBool("RUN", false);
                yvelocity += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
        
        yvelocity += gravity * Time.deltaTime;
        Vector3 playerVelocity = new Vector3(0, yvelocity, 0);
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        MP = hit.gameObject.GetComponent<MovingPlatform>();
    }

    private void Update()
    {
        if (transform.position.y < -5 && GameManager.Instance.Transition(Trigger.Death))
        {
            audioSource.PlayRandom(clipFallen);
        }
    }
}
