using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 30f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;

    private bool canMove = true;
    
    public GameObject characterModel;
    private Animator characterAnimator;

    public Transform pivot;
    public float rotateSpeed;

    public static bool dialogue = false;

    private AudioSource audioSource;
    
    public static bool isWalking = false;
    public static bool isJumping;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterAnimator = characterModel.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 forward = playerCamera.transform.TransformDirection(Vector3.forward);
        Vector3 right = playerCamera.transform.TransformDirection(Vector3.right);

        bool isRunning = false;

        float curSpeedX = 0;
        float curSpeedY = 0;

        isWalking = false;
        

        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");
        if (!dialogue)
        {
            if (inputVertical != 0 || inputHorizontal != 0)
            {
                curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * inputVertical : 0;
                curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * inputHorizontal : 0;
                isWalking = true;
                
            }
            else
            {
                isWalking = false;
            }
        }

        
        
        
        

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }
        
        
        characterAnimator.SetBool("isJumping", isJumping);
        characterAnimator.SetBool("isWalking", isWalking);
        characterAnimator.SetBool("isRunning", isRunning);
        characterController.Move(moveDirection * Time.deltaTime);
        
        //move player in different directions based on camera look direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            characterModel.transform.rotation = Quaternion.Slerp(characterModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        
        
        
    }


}
