using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int coinValue = 1;
    public float Speed = 4;
    public float JumpForce = 8;
    private Rigidbody2D rig;
    public Animator anim;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundlayer;
    public AudioSource AudioSourceJump;
    public AudioSource AudioSourceCoin;

    public GameOverScreen GameOverScreen;
    public scoreManager scoreManager;
    public WinScreen WinScreen;
    public FollowCamera FollowCamera;
    public DialogueController DialogueController;

    public bool hasPassed = false;

    private bool canMove = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        StartCoroutine(CheckWin());
        PassScene();
        CheckDeath();
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
        if (canMove)
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

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            AudioSourceJump.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coins"))
        {
            Destroy(other.gameObject);
            AudioSourceCoin.Play();
            scoreManager.instance.changeScore(coinValue);
        }
    }

    void PassScene()
    {
        if (transform.position.x > 52 && transform.position.y > 0 && !hasPassed)
        {
            hasPassed = true;
            transform.position = new Vector3(86.692f, -6.239f, 0f);
        }
    }

    void CheckDeath()
    {
        if (!hasPassed && transform.position.y < -11.5)
        {
            GameOverScreen.Setup();
            canMove = false;
            transform.position = new Vector3(-43.92f, -8.91f, 0f);
        }
        if (hasPassed && transform.position.y < -7.3 && transform.position.y > -8)
        {
            GameOverScreen.Setup();
            canMove = false;
            transform.position = new Vector3(-43.92f, -8.91f, 0f);
        }
    }

    IEnumerator CheckWin()
    {
        if (transform.position.x > 173 && transform.position.y > 4.6)
        {
            canMove = false;
            DialogueController.Setup();
            yield return new WaitForSeconds(3);
            WinScreen.Setup();
        }
    }

    public void RestartGame()
    {
        FollowCamera.RestartCamera();
        hasPassed = false;
        canMove = true;
        scoreManager.RestartPoints();
        GameOverScreen.RestartButton();
        DialogueController.RestartButton();
        WinScreen.PlayAgainButton();
        transform.position = new Vector3(-43.92f, -8.91f, 0f);
    }
}
