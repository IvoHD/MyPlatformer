using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    float moveSpeed = 1.5f;
    Rigidbody2D enemyRigidbody;
    EnemyHealthStateScript enemyHealthStateScript;
    CapsuleCollider2D capsuleCollider;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyHealthStateScript = GetComponent<EnemyHealthStateScript>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if(enemyHealthStateScript.isAlive)
            enemyRigidbody.velocity = new Vector2(moveSpeed, 0);    
        else
		{
            enemyRigidbody.velocity = Vector2.zero;
            Destroy(capsuleCollider);
		}
    }

    /// <summary>
    /// Flips sprite depending on which derection the enemy is facing
    /// </summary>
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
