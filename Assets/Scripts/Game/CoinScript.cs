using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class CoinScript : MonoBehaviour, ISaveable
{
	public ObjectType GetObjectType()
	{
		return ObjectType.Coin;
	}

	public Vector3 GetPositionToSave()
	{
		return transform.position;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{ 
			SoundManager.instance.PlaySound(Sound.Coin);
			ScoreKeepScript.instance.IncreaseScore(Score.Coin);
			Destroy(gameObject);
		}
	}
}
