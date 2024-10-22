using UnityEngine;
using Assets.Scripts.Enums;

public class PlayerHealthStateScript : MonoBehaviour, IKillable
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.collider.tag == "Enemies" || collision.collider.tag == "Spikes" || collision.collider.tag == "Projectile")
            Kill();
	}

	/// <summary>
	/// Kills player
	/// </summary>
	public void Kill()
    {
        if (isAlive)
        {
            animator.SetTrigger("Killed");

            playerRigidbody.velocity = new Vector2(5f * (Mathf.Sign(playerRigidbody.velocity.x + 1)), 5f);
            playerRigidbody.sharedMaterial = deathMaterial;

            particleSystem.Play();

            SoundManager.instance.PlaySound(Sound.Death);
            ScoreKeepScript.instance.IncreaseScore(Score.Death);

            Invoke("DestroyThisGameObject", 2f);
        }
        isAlive = false;
    }

    /// <summary>
    /// Destroys player gameObject
    /// </summary>
	public void DestroyThisGameObject()
    {
        ReloadScene();
    }

    public void ReloadScene()
	{
        GameManager.instance.ReloadScene();
    }
}
