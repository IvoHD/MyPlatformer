using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthStateScript : MonoBehaviour, IKillable
{
    public bool isAlive = true;
    SpriteRenderer enemySprite;
    Rigidbody2D enemyRigidbody;
    Color32 deathColor = new Color32(255, 0, 0, 255);

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyRigidbody.IsTouchingLayers(LayerMask.GetMask("Projectile")))
            Kill();
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.collider.tag == "Projectile")
            Kill();
    }
	public void Kill()
    {
        isAlive = false;

        enemySprite.color = deathColor;
        Invoke("DestroyThisGameObject", 3);
    }

    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }
}
