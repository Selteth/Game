using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [Header("Horizontal movement")]
    // Amount of force added to move the player left and right.
    public float moveForce = 100f;
    // The fastest the player can travel in the x axis.
    public float maxSpeed = 5f;
    // Horizontal movement axis
    private float horizontalAxis;

    [Header("Vertical movement")]
    // Amount of force added to make the player jump
    public float jumpSpeed = 0.5f;
    // Maximum time the player can be rise up when jumping
    public float maxJumpTime = 2f;
    // Maximum time counter
    private float jumpTimeCounter = 0f;
    
    // Instead of creating special GameObject on those sides, we need, we should better use a vector math. 
    // It is becouse our player object will rotate in the jump.
    private float downVectorLength = 0f;
    public float downVectorAdditionalCoef = 0.05f;
    // Whether the player is on the ground
    private bool isGrounded = false;
    // Whether the player should jump
    private bool shouldJump = false;
    // Whether the player is jumping at the moment
    private bool isJumping = false;

    [Header("Rotate speed")]
    //
    public float angleRotatingImpulse = 10f;
    // The vector around which the player rotates
    //private Vector3 rotateCenter = new Vector3(0, 0, 1);
    private bool isRotating = false;
    
    // Used new to disable warning about hiding base member
    private new Rigidbody2D rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downVectorLength = GetComponent<BoxCollider2D>().size.x * gameObject.transform.localScale.x / 2;

    }

    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        HandleVerticalInput();
        if (isGrounded)
            isRotating = false;

    }

    void FixedUpdate()
    {
        MoveHorizontal();
        MoveVertical();
        Rotate();
    }

    public void ChangeSpeed(float multiplier)
    {
        moveForce *= multiplier;
        maxSpeed *= multiplier;
    }

    public bool IsRising()
    {
        return !isGrounded && rigidbody.velocity.y > 0;
    }

    public bool IsFalling()
    {
        return !isGrounded && rigidbody.velocity.y < 0;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If player is on the ground...
        if (other.gameObject.layer==LayerMask.NameToLayer("Ground"))
        {
            // ...then he might jump again
            isGrounded = true;
            /* Reset rising up variables */
            isJumping = false;
            jumpTimeCounter = 0f;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false; 
        }
    }

    // Handles player input that belongs to jumps
    private void HandleVerticalInput()
    {
        // If player typed jump button and maybe is holding it...
        if (Input.GetButton("Jump"))
        {
            // ...then he should jump
            shouldJump = true;
        }
        else
        {
            shouldJump = false;
            // If player released jump button in the air then hero should stop rising up.
            // It helps to avoid double or triple jump
            isJumping = false;
        }

        
    }

    // Handles horizontal movement
    private void MoveHorizontal()
    {
        if (horizontalAxis * rigidbody.velocity.x < maxSpeed)
            rigidbody.AddForce(Vector2.right * horizontalAxis * moveForce);

        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
    }

    // Handles vertical movement (jumping)
    private void MoveVertical()
    {
        // If jump button was pressed and player is on the ground...
        if (shouldJump && isGrounded)
        {
            // ...then start jumping
            isJumping = true;
            isGrounded = false;
        }

        // If jump was started, and player is holding jump key, and he can rise up...
        if (isJumping && shouldJump && jumpTimeCounter < maxJumpTime)
        {
            // ...change his vertical speed...
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            // ...and decrease time in the air
            jumpTimeCounter += Time.fixedDeltaTime;
        }
    }

    // Handles rotation
    private void Rotate()
    {
        if (!isGrounded && !isRotating)
        {
            rigidbody.AddForceAtPosition(
                new Vector2(transform.position.x+1, transform.position.y), 
                Vector2.left*angleRotatingImpulse*(horizontalAxis>0?(horizontalAxis<0?0:1):-1)
                );
            isRotating = true;
        }
    }
}
