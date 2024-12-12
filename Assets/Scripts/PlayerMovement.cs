using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce = 18f;
    [SerializeField] float runSpeed = 500f;
    float dirX;
    Rigidbody2D rb;
    BoxCollider2D collider2D;
    [SerializeField] LayerMask groundMask;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool gamePaused = false;
    private enum MovementState {idle,run,jump,fall}

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
        }

        HandleAnimation();
        PauseGame();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
        }

        if (gamePaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX * runSpeed * Time.deltaTime, rb.velocity.y, 0f);
    }

    bool isGrounded()
    {
        return Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, 0.1f, groundMask);
    }

    void HandleAnimation()
    {
        MovementState state;
        if (dirX > 0)
        {
            spriteRenderer.flipX = false;
            state = MovementState.run;
        }
        else if (dirX < 0)
        {
            spriteRenderer.flipX = true;
            state = MovementState.run;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        animator.SetInteger("state", (int)state);
    }
}
