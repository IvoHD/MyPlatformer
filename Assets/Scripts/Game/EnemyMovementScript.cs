using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour, ISaveable
{
    Vector3 positionToSave;

    float moveSpeed = 1.5f;
    Rigidbody2D enemyRigidbody;
    EnemyHealthStateScript enemyHealthStateScript;
    CapsuleCollider2D capsuleCollider;

    void Start()
    {
        positionToSave = transform.position;
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
            Destroy(enemyRigidbody);
            Destroy(capsuleCollider);
            Destroy(this);
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

	public Vector3 GetPositionToSave()
	{
        return positionToSave;
    }

    public ObjectType GetObjectType()
	{
        return ObjectType.Slime;
	}
}
