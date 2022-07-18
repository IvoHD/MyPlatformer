using UnityEngine;
using Assets.Scripts.Enums;

public class LevelExitScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		ScoreKeepScript.instance.IncreaseScore(Score.NextLevel);
		Invoke("LoadNextLevel", 2f);
	}

	void LoadNextLevel()
	{
		GameManager.instance.LoadNextLevel();
	}
}
