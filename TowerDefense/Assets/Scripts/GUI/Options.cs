using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour 
{
	string[] qualities;
	Resolution[] resolutions;
	[SerializeField]
	Transform resoPanel;
	[SerializeField]
	Transform qualPanel;
	[SerializeField]
	GameObject modelButton;

	void Start () 
	{
		resolutions = Screen.resolutions;
		for(int i = 0; i < resolutions.Length; i++)
		{
			GameObject button = (GameObject)Instantiate(modelButton);
			button.GetComponentInChildren<Text>().text = ResToString(resolutions[i]);

			int index = i;
			button.GetComponent<Button>().onClick.AddListener
			(
				() => SetResolutionIndex(index)
			);

			button.transform.SetParent(resoPanel);
			button.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
		}

		qualities = QualitySettings.names;
		for (int i = 0; i < qualities.Length; i++)
		{
			GameObject button = (GameObject)Instantiate(modelButton);
			button.GetComponentInChildren<Text>().text = qualities[i];
			int index = i;
			button.GetComponent<Button>().onClick.AddListener
			(
				() => SetQualityIndex(index)
			);

			button.transform.SetParent(qualPanel);
			button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		}
	}
	
	void SetResolutionIndex(int index)
	{
		Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
	}

	void SetQualityIndex(int index)
	{
		QualitySettings.SetQualityLevel(index, false);
	}

	string ResToString(Resolution res)
	{
		return res.width + "x" + res.height;
	}
}
