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
    CapsuleCollider2D capsuleCollider;  //body
    BoxCollider2D boxCollider;          //feet

    float jumpHeight = 8f;
    int JumpAmount;
    int currJumpAmount;
    float fallingFactor = 10f;
    float holdSpaceFactor = 5f;

    bool isRunning;

    float timeBetweenJumps = 0.5f;
    float timeSinceJump;

    ProjectileBehaviousScript storedBullet;

    PlayerHealthStateScript healthStateScript;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        healthStateScript = GetComponent<PlayerHealthStateScript>();

        //Different max amount of jumps depending on current level
        if (GameManager.instance.getCurrentLevel() >= 9)
            JumpAmount = currJumpAmount = 2;
        else
            JumpAmount = currJumpAmount = 1;

        Cursor.visible = false;
    }

    void Update()
    {
        //deactivates movement when player is dead
        if (healthStateScript.isAlive)
        {
            Run();
            if (currJumpAmount < 1 + JumpAmount)
                timeSinceJump -= Time.deltaTime;
            CheckGrounding();
            OnClimb();
            DynamicJump();
        }
        else
        {
            Destroy(gameObject.GetComponent<PlayerInput>());
        }
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Projectile" && storedBullet is null)
		{
            ProjectileBehaviousScript projectileBehaviousScript = collision.GetComponent<ProjectileBehaviousScript>();
            projectileBehaviousScript.ActivateJuggling(this);
            storedBullet = projectileBehaviousScript;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
        if (other.tag == "Projectile" && storedBullet is not null)
		{
            other.gameObject.GetComponent<ProjectileBehaviousScript>().DeactivateJuggling();
            storedBullet = null;
		}
    }

    void OnShoot()
	{
        if (storedBullet is null)
            return;
        storedBullet.ShootBullet(transform.localScale.x < 0);
	}

	public float GetYJugglePosition()
	{
        return transform.position.y + 1f;
	}

	/// <summary>
	/// resets currJumpAmount when player is grounded and not in a jump
	/// </summary>
	void CheckGrounding()
    {
        if (timeSinceJump > 0)
            return;
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climbing", "Towers")))
            currJumpAmount = JumpAmount;
    }

    void Run()
    {
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

    /// <summary>
    /// gets vector from input action
    /// </summary>
    /// <param name="value"></param>
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    /// <summary>
    /// makes the player jump if all conditions for jumping are met
    /// </summary>
    /// <param name="value"></param>
    void OnJump(InputValue value)
    {
        if (!(currJumpAmount > 0))
            return;
        if (timeSinceJump > 0)
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
    /// Better jumping inspired by: https://www.youtube.com/watch?v=7KiK0Aqtmzc, https://www.youtube.com/watch?v=hG9SzQxaCm8&t=0s
    /// </summary>
    void DynamicJump()
    {
        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector2.down * gravity * fallingFactor * Time.deltaTime;
        }
        else if (playerRigidbody.velocity.y > 0 && Input.GetKey(KeyCode.Space))
            playerRigidbody.velocity += Vector2.up * holdSpaceFactor * gravity * Time.deltaTime;

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
