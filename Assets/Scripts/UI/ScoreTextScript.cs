using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField] TMP_Text label;

	public static GameObject instance = null;

	private void Awake()
	{
		if (instance is null)
		{
			instance = gameObject;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}

	public void SetNewScore(int score)
	{
		label.text = score.ToString();
	}
}
