using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    CharacterController charCont;

    private Vector3 moveDirection = Vector3.zero;
    public float speed = 10f;
    public float gravity = 20f;
    public float jumpSpeed = 8f;
    private float verticalVelocity;
    #endregion


    public Animator anim;
    public int damage;
    public Rigidbody projectile;
    public float shootSpeed;
    private float jumpStart = 0f;
    public float jumpCooldown = 1f;
    private float attackStart = 0f;
    public float attackCooldown = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        charCont = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (Input.GetButtonDown("Fire2") && (Time.time > attackStart + attackCooldown))
        {            
            PlayerAttack2();
        }
        //attack anim
        bool Attack1 = Input.GetButtonDown("Fire1");
        anim.SetBool("Attack1", Attack1);
        bool Attack2 = Input.GetButtonDown("Fire2");
        anim.SetBool("Attack2", Attack2);
    }

    void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //transform deals with a specific axis and the movement on that axis while considering its rotation
        //Time.deltaTime can be used to make something happen at a constant rate regardless of the (possibly wildly fluctuating) framerate. e.g. projectiles, movement
        moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection *= speed;

        if (Input.GetButton("Sprint") && (moveZ != 0 || moveX != 0))
        {        
            speed = 10f;
            anim.SetBool("isRunning", true);
        }
        else
        {
            speed = 5f;
            anim.SetBool("isRunning", false);
        }

        //Everything in here runs ONLY if the player is on the ground during the current frame
        if (charCont.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (moveZ != 0 || moveX != 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            anim.SetFloat("Speed", moveZ);
            anim.SetBool("Jump", false);

            if (Input.GetButton("Jump") && (Time.time > jumpStart + jumpCooldown))
            {
                verticalVelocity = jumpSpeed;
                anim.SetBool("Jump", true);
                jumpStart = Time.time;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = 0;
        moveDirection.y = verticalVelocity;

        charCont.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (Input.GetButtonDown("Fire1") && (Time.time > attackStart + attackCooldown))
        {
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<CharHealth>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("hit");
                other.gameObject.GetComponent<Destructible>().TakeDamage(damage);
            }
        }
    }

    void PlayerAttack2()
    {        
        Rigidbody clone;
        Vector3 startPosition;

        startPosition = transform.position;
        startPosition.y += 1f;

        clone = Instantiate(projectile, startPosition + transform.forward * 2f, transform.rotation);

        clone.velocity = transform.forward * shootSpeed;
        attackStart = Time.time;
    }
}
