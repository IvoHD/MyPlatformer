using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsScreenScript : MonoBehaviour
{
	[SerializeField]
	Toggle fullScreenToggle;
	[SerializeField]
	TMP_Text resLabel;

	[SerializeField]
	public List<ResItem> resItems = new List<ResItem>();
	int selectedResIndex;

	[SerializeField]
	AudioMixer mixer;
	[SerializeField]
	Toggle volumeToggle;
	[SerializeField]
	Slider volumeSlider;
	[SerializeField]
	TMP_Text volumeText;

	private void Start()
	{
		fullScreenToggle.isOn = Screen.fullScreen;

		//finds and sets/creates currents player resolution
		bool foundRes = false;
		for(int i = 0; i < resItems.Count; i++)
		{
			if(Screen.width == resItems[i].width && Screen.height == resItems[i].height)
			{
				foundRes = true;
				selectedResIndex = i;
				UpdateResLabel();
			}
		}

		if(!foundRes)
		{
			resItems.Insert(0, new ResItem(Screen.width, Screen.height));

			selectedResIndex = 0;

			UpdateResLabel();
		}

		//sets current Audiovalue
		if (PlayerPrefs.HasKey("MasterVol"))
		{
			float valueOnBoot = PlayerPrefs.GetFloat("MasterVol");
			mixer.SetFloat("MasterVol", valueOnBoot);
			volumeSlider.value = valueOnBoot;
		}
		else
		{
			mixer.SetFloat("MasterVol", 20);
			volumeSlider.value = 20;
		}
	}

	public void LowerRes()
	{
		selectedResIndex--;
		if (selectedResIndex < 0)
		{
			selectedResIndex = 0;
			return;
		}
		UpdateResLabel();
	}

	public void IncreaseRes()
	{
		selectedResIndex++;
		if (selectedResIndex > resItems.Count - 1)
		{
			selectedResIndex = resItems.Count - 1;
			return;
		}
		UpdateResLabel();
	}

	void UpdateResLabel()
	{
		resLabel.text = resItems[selectedResIndex].width.ToString() + " X " + resItems[selectedResIndex].height.ToString();
	}

	 
	public void setVolume()
	{

		volumeText.text = Mathf.RoundToInt(volumeSlider.value + 80).ToString();
	}

	public void ApplyChanges()
	{
		Screen.SetResolution(resItems[selectedResIndex].width, resItems[selectedResIndex].height, fullScreenToggle.isOn);
		if (volumeToggle.isOn)
		{
			volumeSlider.value = -80;
			mixer.SetFloat("MasterVol", -80);
			PlayerPrefs.SetFloat("MasterVol", -80);
		}
		else
		{
			mixer.SetFloat("MasterVol", volumeSlider.value);
			PlayerPrefs.SetFloat("MasterVol", volumeSlider.value);
		}
	}
}

[System.Serializable]
public class ResItem
{
	public int width;
	public int height;

	public ResItem(int width, int height)
	{
		this.width = width;
		this.height = height;
	}
}