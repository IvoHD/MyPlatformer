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
	TMP_Text volumeLabel;

	private void Start()
	{
		fullScreenToggle.isOn = Screen.fullScreen;

		//finds and sets/creates currents player resolution
		bool foundRes = false;
		for(int i = 0; i < resItems.Count; i++)
		{
			if(Screen.width == resItems[i]._width && Screen.height == resItems[i]._height)
			{
				foundRes = true;
				selectedResIndex = i;
			}
		}

		if(!foundRes)
		{
			resItems.Insert(0, new ResItem(Screen.width, Screen.height));

			selectedResIndex = 0;
		}
		UpdateResLabel();

		float f;
		mixer.GetFloat("MasterVol", out f);
		volumeSlider.value = f;

		UpdateVolumeLabel();
	}

	/// <summary>
	/// decrements resolution preset and displays new value
	/// </summary>
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

	/// <summary>
	/// increments resolution preset and displays new value
	/// </summary>
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
		resLabel.text = resItems[selectedResIndex]._width.ToString() + " X " + resItems[selectedResIndex]._height.ToString();
	}

	public void UpdateVolumeLabel()
	{ 
		volumeLabel.text = Mathf.RoundToInt(volumeSlider.value + 80).ToString();
	}


	/// <summary>
	/// Save all graphic and audio changes
	/// </summary>
	public void ApplyChanges()
	{
		Screen.SetResolution(resItems[selectedResIndex]._width, resItems[selectedResIndex]._height, fullScreenToggle.isOn);
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
	public int _width;
	public int _height;

	public ResItem(int width, int height)
	{
		_width = width;
		_height = height;
	}
}