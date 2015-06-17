﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour 
{
	public RawImage ImagePause;
	public Text InfoText;

	private bool _pause = false;

	void Update () 
	{
		if(LevelStart.instance.modeMulti == false)
		{
			if (_pause == true)
			{
				ImagePause.gameObject.SetActive(true);
				Time.timeScale = 0.0f;
				InfoText.text = "Pause";
				if (Input.GetKeyDown(KeyCode.Escape))
					_pause = false;

			}
			else
			{
				ImagePause.gameObject.SetActive(false);
				Time.timeScale = 1;
				InfoText.text = "";
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					_pause = true;
				}
			}
		}
	}
}
