using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour 
{
	public RawImage ImagePause;
	public Text InfoText;

	private bool _pause = false;
	private CameraController _camera;
	void Start()
	{
		_camera = Camera.main.gameObject.GetComponent<CameraController>();
	}

	void Update () 
	{
		if(LevelStart.instance.modeMulti == false)
		{
			if (_pause == true)
			{
				ImagePause.gameObject.SetActive(true);
				Time.timeScale = 0.0f;
				_camera.enabled = false;
				InfoText.text = "Pause";
				if (Input.GetKeyDown(KeyCode.Escape))
					_pause = false;

			}
			else
			{
				ImagePause.gameObject.SetActive(false);
				Time.timeScale = 1;
				_camera.enabled = true;
				InfoText.text = "";
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					_pause = true;
				}
			}
		}
	}
}
