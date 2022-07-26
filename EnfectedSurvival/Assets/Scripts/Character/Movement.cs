using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Movement Options
    public float moveSpeed;
    private float holder;

    //COMPONENTS
    Rigidbody2D rigidBody;
    public Camera cam;
    Transform my;
    private Health health;
   
    //Vectors
    Vector2 movement;
    Vector2 mousePos;

    //Animation
    private Animator animator;

    //Particle
    public ParticleSystem footprint,dust;

    void Start()
    {
        //rb
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        //transform
        my = gameObject.GetComponent<Transform>();
        //animator
        animator = gameObject.GetComponent<Animator>();
        //health
        health = this.GetComponent<Health>();
    }

    void Update()
    {
        //Yön tanım
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //animation trigger
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") !=0)
        {

            animator.SetBool("isMoving", true);  
        }
        else
        {
           
            animator.SetBool("isMoving", false);      
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            WalkingParticles(true);
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            WalkingParticles(false);
        }

        //Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run(true);
        }
        else
        {
            Run(false);
        }



        //Mouse Position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        //MOVEMENT
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        //Look to Mouse
        Vector2 bakmaYon = mousePos - rigidBody.position;
        float aci = Mathf.Atan2(bakmaYon.y, bakmaYon.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = aci;
    }
    public void WalkingParticles(bool play)//particle effect
    {
        if (play)
        {
            dust.Play();
            footprint.Play();
        }
        else
        {
            dust.Stop();
            footprint.Stop();
        }
    }
    public void Run(bool isRunning)
    {
        if (holder == 0)//set holder
            holder = moveSpeed;
        if (isRunning && health.stamina > 0)
        {
            moveSpeed = holder * 1.5f;//increase speed

            health.DecreaseStamina(1);//decrease stamina
        }
        else
        {
            moveSpeed = holder;//normalize movespeed
            if (health.stamina < health.maxStamina)
            {
                health.DecreaseStamina(-1);//increase stamina
            }
        }


    }
}
