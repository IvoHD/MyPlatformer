using Assets.Scripts.Enums;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		ScoreKeepScript.instance.IncreaseScore(Score.Coin);
		Destroy(gameObject);
	}
}
