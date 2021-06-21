using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed = 4;
    public float JumpForce = 8;
    private Rigidbody2D rig;
    public Animator anim;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundlayer;
    public AudioSource AudioSourceJump;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
        if (isGrounded)
        {
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", true);
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("run", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("run", false);
        }

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            AudioSourceJump.Play();
        }
    }
}
