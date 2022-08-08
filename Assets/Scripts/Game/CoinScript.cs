using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class CoinScript : MonoBehaviour, ISaveable
{
	void Start()
	{
		Register();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{ 
			SoundManager.instance.PlaySound(Sound.Coin);
			ScoreKeepScript.instance.IncreaseScore(Score.Coin);
			Deregister();
			Destroy(gameObject);
		}
	}

	public ObjectType GetObjectType()
	{
		return ObjectType.Coin;
	}

	public Vector3 GetPositionToSave()
	{
		return transform.position;
	}

	public void Register()
	{
		SavingManager.instance.Register(this);
	}

	public void Deregister()
	{
		SavingManager.instance.Deregister(this);
	}

}
