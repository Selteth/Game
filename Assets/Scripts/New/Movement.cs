using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float maxJumpTime;
    public float waitStartTime;
    
    private Transform jumpCheck;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private float jumpTimeCounter = 0f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool shouldJump = false;

    private void Awake()
    {
        DisableMoving();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCheck = transform.Find("JumpCheck");
        StartCoroutine("WaitBeforeStart");
    }

    private void Start()
    {
        runSpeed = GameObject.Find("Difficulty").GetComponent<Difficulty>().GetSpeed();
    }
    
    private void Update()
    {
        HandleVerticalInput();
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("runSpeed", runSpeed);
    }

    private void FixedUpdate()
    {
        Jump();
        playerRigidbody.velocity = new Vector2(runSpeed, playerRigidbody.velocity.y);
    }

    public void DifficultySwitched(float newSpeed)
    {
        runSpeed = newSpeed;
    }

    public bool IsFalling()
    {
        return !isGrounded && playerRigidbody.velocity.y < 0;
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(waitStartTime);
        EnableMoving();
    }

    private void DisableMoving()
    {
        enabled = false;
    }

    private void EnableMoving()
    {
        enabled = true;
    }

    private void HandleVerticalInput()
    {
        if (Input.GetButton("Jump"))
        {
            shouldJump = true;
        }
        else
        {
            shouldJump = false;
            isJumping = false;
        }

        if (IsGrounded())
        {
            isGrounded = true;
            isJumping = false;
            jumpTimeCounter = 0f;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if (shouldJump && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
        }

        if (isJumping && shouldJump && jumpTimeCounter < maxJumpTime)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
            jumpTimeCounter += Time.fixedDeltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Linecast(transform.position, jumpCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

}
