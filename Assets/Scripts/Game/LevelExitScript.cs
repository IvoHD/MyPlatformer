using UnityEngine;
using Assets.Scripts.Enums;

public class LevelExitScript : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		SoundManager.instance.PlaySound(Sound.NextLevel);
		ScoreKeepScript.instance.IncreaseScore(Score.NextLevel);
		Destroy(gameObject.GetComponent<Collider2D>());
		Invoke("LoadNextLevel", 2f);
	}

	void LoadNextLevel()
	{
		GameManager.instance.LoadNextLevel();
	}
}
