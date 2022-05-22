using UnityEngine;
using UnityEngine.InputSystem;


#pragma warning disable IDE0051 // Nicht verwendete private Member entfernen

public class PlayerMovementScript : MonoBehaviour
{
    float runSpeed = 5f;
    float jumpHeight = 12f;
    Animator animator;
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    CapsuleCollider2D capsuleCollider;

    bool hasUnlockedDoubleJump = true;
    int jumpAmount = 1;
    int bonusJumpAmount = 0;
    bool isRunning;

	// Start is called before the first frame update
	void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
    
	void Run()
	{
        //disables the possibilty to run upwards/downwards
		Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed * (isRunning ? 1.5f : 1), playerRigidbody.velocity.y);
		playerRigidbody.velocity = playerVelocity;

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


    void OnJump()
    {
        if(!(jumpAmount > 0))
            return;
    

        jumpAmount--;
        playerRigidbody.velocity += new Vector2(0, jumpHeight);

    }

    /// <summary>
    /// Toggle runningstate
    /// </summary>
    void OnIsRunning()
	{
        isRunning = !isRunning;
	}

    bool IsMoving()
	{
        return Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
    }



	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Ground")
		{
            jumpAmount = 1 + bonusJumpAmount;
		}
		
	}

    void BonusJumpUp()
	{
        bonusJumpAmount++;
	}
}
