using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseEvents : MonoBehaviour 
{
	Color color;
	public void OnPointerEnter()
	{
		color = GetComponent<RawImage>().color;
		GetComponent<RawImage>().color = new Color32(181, 235, 235, 255);

	}
	public void OnPointerExit()
	{
		GetComponent<RawImage>().color = color;
	}

	public void OnPointerEnterText()
	{
		color = GetComponent<Text>().color;
		GetComponent<Text>().color = new Color32(116, 16, 37, 255);
	}

	public void OnPointerExitText()
	{
		GetComponent<Text>().color = color;
	}
}
