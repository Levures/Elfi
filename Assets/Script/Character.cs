using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    Camera cam;
    CharacterController characterController;
    float maxSpeed = 10, acceleration = 10, jumpForce = 5;
    float speed, verticalMovement;
    Vector3 direction, directionForward, directionRight, nextDir;
    Animator animator;
    [SerializeField]
    AudioClip[] dirtStepSounds, floorStepSounds;

    private AudioClip[] stepSound;

    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        direction = transform.forward;
        nextDir = transform.forward;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        gravity();

        Move();

        CheckGround();


        //apply the calculated movement to the character controller movement system
        characterController.Move((direction * speed + verticalMovement * Vector3.up) * Time.deltaTime);

        animator.SetFloat("Speed", speed / maxSpeed);
    }

    private void Move()
    {
        if ((Input.GetAxisRaw("Vertical")) != 0 || (Input.GetAxisRaw("Horizontal")) != 0)
        {
            //gets the inputs from keyboard arrows and defines the direction depending on the camera's orientation;

            directionForward = cam.transform.forward;
            directionForward.y = 0;
            directionForward *= Input.GetAxisRaw("Vertical");

            directionRight = cam.transform.right;
            directionRight.y = 0;
            directionRight *= Input.GetAxisRaw("Horizontal");

            nextDir = Vector3.Normalize(directionForward + directionRight);

            //Direction interpolation between the current direction and the inputed direction
            direction = Vector3.Lerp(direction, nextDir, Time.deltaTime * 2);

            //Calculate the speed increasement depending on the time spent pushing an arrow button;

            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else
            {
                speed = maxSpeed;
            }

        }
        else
        {
            //Calculate the speed decreasement depending on the time since no arrow button is pressed;

            if (speed != 0)
            {
                if (speed <= 2 * acceleration * Time.deltaTime)
                    speed = 0;
                else
                {
                    speed -= 2 * acceleration * Time.deltaTime;
                }
            }
        }

        //make the object rotate toward its movement;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    private void gravity()
    {
        if (verticalMovement <= 0 && characterController.isGrounded)
        {
            verticalMovement = -5;
        }
        else
        {
            verticalMovement -= jumpForce * 2 * Time.deltaTime;
        }
    }

    // Fonction appelée lors de chaque pas grâce à un animation event intégré dans le cycle de marche du personnage
    public void StepSound()
    {
        // À remplacer lorsque vous intégrerez les sons de pas
        if (stepSound != null)
        {
            myAudioSource.PlayOneShot(stepSound[Random.Range(0, stepSound.Length - 1)]);
        }
        else
        {
            Debug.Log("Il faut intégrer l'audioclip dans le script Character !!!");
        }
    }

    private void CheckGround()
    {
        Physics.Raycast(transform.position, Vector3.down * 10, out RaycastHit hitInfo);
        string groundTag = hitInfo.collider.tag;
        
        
        switch (groundTag)
        {
            case "Terrain":
                stepSound = dirtStepSounds;
                break;
            case "HouseFloor":
                stepSound = floorStepSounds;
                break;
        }
    }
}
