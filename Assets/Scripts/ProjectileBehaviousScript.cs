using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviousScript : MonoBehaviour
{
    float bulletspeed = 7f;
    public bool isFacingLeft;
    float lifeTime = 30f;
    Rigidbody2D projectileRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime < 0)
            Destroy(gameObject);
        lifeTime -= Time.deltaTime;
        projectileRigidbody.velocity = new Vector2(isFacingLeft ? -bulletspeed : bulletspeed, 0f);
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!projectileRigidbody.IsTouchingLayers(LayerMask.GetMask("Background")))
            Destroy(gameObject);
	}
}
