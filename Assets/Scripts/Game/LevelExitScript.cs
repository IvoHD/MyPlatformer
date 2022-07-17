using UnityEngine;
using Assets.Scripts.Enums;

public class LevelExitScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Invoke("LoadNextLevel", 2f);
	}

	void LoadNextLevel()
	{
		ScoreKeepScript.instance.IncreaseScore(Score.NextLevel);
		GameManager.instance.LoadNextLevel();
	}
}
