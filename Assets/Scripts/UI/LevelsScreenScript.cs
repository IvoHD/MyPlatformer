using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreenScript : MonoBehaviour
{
    [SerializeField]
    List<Button> buttons;
    void Start()
    {
        for (int i = 2; i < GameManager.instance.getCurrentLevel(); i++)
		{
            buttons[i - 2].interactable = true;
		}
    }

    public void loadLevel(int levelIndex)
	{
        GameManager.instance.LoadLevel(levelIndex);
	}

}
