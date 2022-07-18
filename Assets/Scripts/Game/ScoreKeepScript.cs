using UnityEngine;
using Assets.Scripts.Enums;

public class ScoreKeepScript : MonoBehaviour
{
    public static ScoreKeepScript m_instance;
    public static ScoreKeepScript instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = new GameObject().AddComponent<ScoreKeepScript>();
                m_instance.name = "ScoreKeeper";
                DontDestroyOnLoad(m_instance.gameObject);
            }
            return m_instance;
        }
    }

    int currScore;
    ScoreTextScript scoreTextScript;

    void Awake()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            currScore = PlayerPrefs.GetInt("score");
        }

        scoreTextScript = GameObject.Find("ScoreCanvas").GetComponent<ScoreTextScript>();
        scoreTextScript.SetNewScore(currScore);
    }

    public void IncreaseScore(Score toAdd)
    { 
        currScore += (int) toAdd;
        Debug.Log(currScore);
        scoreTextScript.SetNewScore(currScore);
        PlayerPrefs.SetInt("score", currScore);
	}

    public void resetScore()
	{
        currScore = 0;
	}

    public int GetScore()
	{
        return currScore;
	}

    public void instatiate() {}
}
