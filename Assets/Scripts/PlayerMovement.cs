using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public float jumpforce;
    private float jumps;
    public float Maxjumps;
    private bool isGrounded;
    public float Dashforce;
    private float Dashes;
    public float Maxdashes;

    private float inputHorizontal;
    private float inputVertical;
    private bool isfacingright;


    Rigidbody2D rb;
    Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // movement left and right
        float move = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        transform.Translate(move, 0, 0);
        // jumping
        if (isGrounded == true && jumps != Maxjumps)
        {
            jumps = Maxjumps;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumps >= 1)
        {
            jumps--;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        // Dashes right
        if (Input.GetKeyDown(KeyCode.LeftShift) && Dashes >= 1 && isfacingright == true)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Dashforce, 0), ForceMode2D.Impulse);
            Dashes--;
        }
        // Dashes left
        if (Input.GetKeyDown(KeyCode.LeftShift) && Dashes >= 1 && isfacingright == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-Dashforce, 0), ForceMode2D.Impulse);
            Dashes--;
        }
        // resets Dashes if on ground
        if (isGrounded == true && Dashes != Maxdashes)
        {
            Dashes = Maxdashes;
        }
    }

    private void FixedUpdate()
    {
        // gets vertical and horizontal inputs
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        // makes player face left if horizontal input less than 0
        if (inputHorizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            isfacingright = false;
        }
        // makes player face right if horizontal input greater than 0
        if (inputHorizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            isfacingright = true;
        }

        // walking and jump animation values stuff
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        animator.SetBool("isJumping", !isGrounded);
    }

    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
