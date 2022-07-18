using UnityEngine;
using Assets.Scripts.Enums;

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
    
    /// <summary>
    /// kills enemy
    /// </summary>
	public void Kill()
    {
        isAlive = false;

        animator.enabled = false;
        enemySprite.color = deathColor;

        SoundManager.instance.PlaySound(Sound.Kill);
        ScoreKeepScript.instance.IncreaseScore(Score.Kill);

        Invoke("DestroyThisGameObject", 1f);
    }

    /// <summary>
    /// Destroys enemy gameObject
    /// </summary>
    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }
}
