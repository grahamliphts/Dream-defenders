using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour 
{
	public RawImage ImagePause;
	public Text InfoText;
	private CameraController _camera;
	private NetworkView _networkView;
	private OpenShop _shop;

	private bool _pause;
	public bool pause
	{
		get
		{
			return _pause;
		}
		set
		{
			_pause = value;
		}
	}

	private bool _isMine;
	public bool isMine
	{
		get
		{
			return _isMine;
		}
		set
		{
			_isMine = value;
		}
	}

	void Start()
	{
		_camera = Camera.main.gameObject.GetComponent<CameraController>();
		_networkView = GetComponent<NetworkView>();
		ImagePause.gameObject.SetActive(false);
		_pause = false;
		_shop = GetComponent<OpenShop>();
	}

	void Update () 
	{
		if (!LevelStart.instance.modeMulti || _isMine)
		{
			if (Input.GetKeyDown(KeyCode.Escape) && !_pause && !_shop.shopOpen)
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
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			InfoText.text = "Pause";
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;
			InfoText.text = "";
		}
	}
}

