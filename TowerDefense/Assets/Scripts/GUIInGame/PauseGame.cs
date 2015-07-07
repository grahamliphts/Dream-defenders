﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour 
{
	public RawImage ImagePause;
	public Text InfoText;

	private bool _pause = false;
	private CameraController _camera;
	private NetworkView _networkView;

	public bool isMine;
	void Start()
	{
		_camera = Camera.main.gameObject.GetComponent<CameraController>();
		_networkView = GetComponent<NetworkView>();
		ImagePause.gameObject.SetActive(false);
	}

	void Update () 
	{
		if(!LevelStart.instance.modeMulti || isMine)
		{
			if (Input.GetKeyDown(KeyCode.Escape) && !_pause)
			{
				if (LevelStart.instance.modeMulti)
					_networkView.RPC("SyncPause", RPCMode.All, true);
				else
					Pause(true);
			}

			else if(Input.GetKeyDown(KeyCode.Escape) && _pause)
			{
				if (LevelStart.instance.modeMulti)
					_networkView.RPC("SyncPause", RPCMode.All, false);
				else
					Pause(false);
			}
		}
	}

	[RPC]
	void SyncPause(bool value)
	{
		Pause(value);
	}

	void Pause(bool value)
	{
		_pause = value;
		ImagePause.gameObject.SetActive(value);
		_camera.enabled = !value; 
		if(value)
		{
			Time.timeScale = 0.0f;
			InfoText.text = "Pause";
		}
		else
		{
			Time.timeScale = 1;
			InfoText.text = "";
		}
	}
}

