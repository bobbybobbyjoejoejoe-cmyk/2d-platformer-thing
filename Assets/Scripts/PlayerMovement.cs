using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public float jumpforce;
    private float jumps;
    public float Maxjumps;
    private bool isGrounded;

    Rigidbody2D rb;

    void Update()
    {
        // movement left and right
        float move = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        transform.Translate(move, 0, 0);
        // jumping
        if (Input.GetKeyDown("space") && jumps >= 1)
        {
            jumps--;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
        if (isGrounded == true && jumps != Maxjumps)
        {
            jumps = Maxjumps;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            jumps--;
        }
    }

}
