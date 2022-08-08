using UnityEngine;
using Assets.Scripts.Enums;

public class ProjectileBehaviousScript : MonoBehaviour
{
    float bulletSpeed = 7f;
    bool isFacingLeft;
    Rigidbody2D projectileRigidbody;
    bool isReflected;
    bool isBeingJuggled;
    PlayerMovementScript playerMovementScript;

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

        if (isBeingJuggled)
        {
            transform.position = new Vector3(transform.position.x, playerMovementScript.GetYJugglePosition());
        }

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

    public void SetBulletSpeed(float bulletspeed)
    {
        bulletSpeed = bulletspeed;
    }

    public void ActivateJuggling(PlayerMovementScript playermovementscript)
    {
        isBeingJuggled = true;
        playerMovementScript = playermovementscript;
        bulletSpeed /= 2;
    }

    public void DeactivateJuggling()
	{
        isBeingJuggled = false;
        bulletSpeed *= 2;
	}

    public void ShootBullet(bool isfacingleft)
	{
        isBeingJuggled = false;
        isFacingLeft = isfacingleft;
        if (isFacingLeft)
            isReflected = false;
        else
            isReflected = false;
        bulletSpeed = 20f;
    }
}
