using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable IDE0051 // Nicht verwendete private Member entfernen

public class PlayerMovementScript : MonoBehaviour
{
    float gravity = 2.5f;

    float runSpeed = 5f;
    float climbSpeed = 3f;
    Animator animator;
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;

    float jumpHeight = 8f;
    int JumpAmount;
    int currJumpAmount;
    float fallingFactor = 10f;
    float holdSpaceFactor = 5f;

    bool isRunning;
    
    float timeBetweenJumps = 0.5f;
    float timeSinceJump;

    PlayerHealthStateScript healthStateScript;

	void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        healthStateScript = GetComponent<PlayerHealthStateScript>();

        //Different max amount of jumps depending on current level
        if(GameManager.instance.getCurrentLevel() >= 9)
            JumpAmount = currJumpAmount = 2;
        else
            JumpAmount = currJumpAmount = 1;
    }

	void Update()
    {

        if (healthStateScript.isAlive)
		{
            Run();
            if (currJumpAmount < 1 + JumpAmount)
                timeSinceJump -= Time.deltaTime;
            CheckGrounding();
            OnClimb();
            OnJump(null);
        }
        else
		{
            JumpAmount = 0;
		}
    }


	void CheckGrounding()
	{
        if (timeSinceJump > 0)
            return;
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing", "Towers", "Climbing")))
            currJumpAmount = JumpAmount;
	}

	

	void Run()
	{
        //disables the possibilty to run upwards/downwards
		playerRigidbody.velocity = new Vector2(moveInput.x * runSpeed * (isRunning ? 1.5f : 1), playerRigidbody.velocity.y);

        animator.SetBool("IsRunning", IsMoving());

        FlipSprite();
	}

    /// <summary>
    /// Flips the sprite depending on the playerRigidbody.velocity.x value
    /// </summary>
	void FlipSprite()
	{
       if (IsMoving())
           //Mathf.Sign(playerRigidboy.velocity.x) returns true/false depending if the veolcity is positve or negative.
           transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x) * 2, 2f);
    }

    void OnMove(InputValue value)
	{
        moveInput = value.Get<Vector2>();
	}

    void OnJump(InputValue value)
    {
        //Dynamic jump
        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector2.down * gravity * fallingFactor * Time.deltaTime;
        }
        else if (playerRigidbody.velocity.y > 0 && Input.GetKey(KeyCode.Space))
            playerRigidbody.velocity += Vector2.up * holdSpaceFactor * gravity * Time.deltaTime;

        if (value is null)
            return;

        if (!(currJumpAmount > 0))
            return;
        if(timeSinceJump > 0)
            return;
        if (JumpAmount == 1 && !boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing", "Towers"))) 
            return;
        currJumpAmount--;
        timeSinceJump = timeBetweenJumps;
        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.velocity += new Vector2(0, jumpHeight);
        playerRigidbody.gravityScale = gravity;

    }


        /// <summary>
        /// Checks if Player is climbing
        /// </summary>
    void OnClimb()
	{
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
	    {
            animator.SetBool("IsClimbing", false);
            playerRigidbody.gravityScale = gravity;
            return;
	    }

        if (Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon)
	    {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsClimbing", true);
	    }
        HandleJumpOnLadder();
    }

    /// <summary>
    /// Handles gravity on ladder. While not jumping gravity = 0
    /// </summary>
	void HandleJumpOnLadder()
	{
        if (timeSinceJump > 0)
		{
            playerRigidbody.gravityScale = gravity;
            animator.SetBool("IsClimbing", false);
		}
        else
        {
            playerRigidbody.gravityScale = 0;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed * (isRunning ? 1.5f : 1));

            if (!(Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon))
                animator.SetBool("IsClimbing", false);
        }
    }

    /// <summary>
    /// Toggle runningState
    /// </summary>
    void OnIsRunning()
	{
        isRunning = !isRunning;
	}

    bool IsMoving()
	{
        return Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
    }

    /// <summary>
    /// Increments bonusJumps
    /// </summary>
    void BonusJumpUp()
	{
        JumpAmount++;
	}

}