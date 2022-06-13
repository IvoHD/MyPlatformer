using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    float moveSpeed = 1.5f;
    Rigidbody2D enemyRigidbody;
    EnemyHealthStateScript enemyHealthStateScript;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyHealthStateScript = GetComponent<EnemyHealthStateScript>();
    }

    void Update()
    {
        if(enemyHealthStateScript.isAlive)
            enemyRigidbody.velocity = new Vector2(moveSpeed, 0);    
        else
            enemyRigidbody.velocity = Vector2.zero;
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1);
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.tag == "Mushroom" || collision.tag == "Towers")
            return;
        moveSpeed = -moveSpeed;
        FlipSprite();
	}
}
