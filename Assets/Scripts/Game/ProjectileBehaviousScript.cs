using UnityEngine;
using Assets.Scripts.Enums;

public class ProjectileBehaviousScript : MonoBehaviour
{
    float bulletSpeed = 7f;
    bool isFacingLeft;
    Rigidbody2D projectileRigidbody;
    bool isReflected;
    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isFacingLeft)
            projectileRigidbody.velocity = new Vector2(isReflected ? bulletSpeed : -bulletSpeed, 0);
        else
            projectileRigidbody.velocity = new Vector2(isReflected ? -bulletSpeed : bulletSpeed, 0);
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Sword")
            Destroy(gameObject);
		else
		{
            isReflected = !isReflected;
            SoundManager.instance.PlaySound(Sound.Reflect);
            ScoreKeepScript.instance.IncreaseScore(Score.Reflect);
		}
	}

    public void SetIsFacingLeft(bool isfacingleft)
	{
        isFacingLeft = isfacingleft;
	}

    public void setBulletSpeed(float bulletspeed)
    {
        bulletSpeed = bulletspeed;
    }
}
