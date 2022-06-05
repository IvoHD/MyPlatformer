using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStateScript : MonoBehaviour, IKillable
{
    public bool isAlive = true;
    Rigidbody2D playerRigidbody;
    Animator animator;

    [SerializeField]
    PhysicsMaterial2D noFrictionMaterial;
    [SerializeField]
    PhysicsMaterial2D deathMaterial;

    ParticleSystem particleSystem;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (playerRigidbody.IsTouchingLayers(LayerMask.GetMask("Enemies")) && isAlive)
            Kill();
		
	}

    /// <summary>
    /// Kills player
    /// </summary>
    public void Kill()
    {
        isAlive = false;

        animator.SetTrigger("Killed");

        playerRigidbody.velocity = new Vector2(5f * (Mathf.Sign(playerRigidbody.velocity.x + 1)), 5f);
        playerRigidbody.sharedMaterial = deathMaterial;

        particleSystem.Play();

        Invoke("DestroyThisGameObject", 2f);
    }

    /// <summary>
    /// Destroys this GameObject
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
	public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }
}
