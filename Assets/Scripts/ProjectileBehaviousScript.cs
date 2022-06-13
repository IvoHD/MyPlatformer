using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviousScript : MonoBehaviour
{
    float bulletspeed = 7f;
    public bool isFacingLeft;
    float lifeTime = 30f;
    Rigidbody2D projectileRigidbody;

    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
        projectileRigidbody.velocity = new Vector2(isFacingLeft ? -bulletspeed : bulletspeed, 0f);
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
	}
}
