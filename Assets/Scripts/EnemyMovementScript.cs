using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    float moveSpeed = 1.5f;
    Rigidbody2D enemyRigidbody; 
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRigidbody.velocity = new Vector2(moveSpeed, 0);    
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1);
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        moveSpeed = -moveSpeed;
        FlipSprite();
	}
}
