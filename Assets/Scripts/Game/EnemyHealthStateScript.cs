using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthStateScript : MonoBehaviour, IKillable
{
    public bool isAlive = true;
    SpriteRenderer enemySprite;
    Color32 deathColor = new Color32(255, 0, 0, 255);

    Animator animator;


    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.collider.tag == "Projectile" || collision.collider.tag == "Sword")
            Kill();
    }

	public void Kill()
    {
        isAlive = false;

        animator.enabled = false;
        enemySprite.color = deathColor;
        Invoke("DestroyThisGameObject", 1f);
    }

    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }
}
