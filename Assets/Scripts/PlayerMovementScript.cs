using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    float runSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

	void Run()
	{
        //disables the possibilty to run upwards/downwards
		Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
		playerRigidbody.velocity = playerVelocity;

		FlipSprite();
	}

	 void FlipSprite()
	 {
        if (MathF.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
     }

    void OnMove(InputValue value)
	{
        moveInput = value.Get<Vector2>();
	}
}
