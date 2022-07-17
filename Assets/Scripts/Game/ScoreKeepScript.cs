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

    // Start is called before the first frame update
    void awake()
    {
        //playerprefs
        currScore = 0;
    }

    public void IncreaseScore(Score toAdd = 0)
	{
        if (toAdd > 0)
            currScore += (int) toAdd;
        Debug.Log(currScore);
	}



}
