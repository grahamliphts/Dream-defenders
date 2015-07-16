using UnityEngine;
using System.Collections;

public class OpenShop : MonoBehaviour 
{
	public GameObject guiCanvas;
	public GameObject shopCanvas;
	public ShopManager shop;
	private bool _shopOpen;
	public bool shopOpen
	{
		get
		{
			return _shopOpen;
		}
		set
		{
			_shopOpen = value;
		}
	}
	private CameraController _camera;
	private PauseGame _pauseGame;

	//player objects
	public GameObject player;
	private SpellController _spellController;
	private TowerController _towerController;
	private CharacController _characController;
	private bool _init;

	void Start()
	{
		_shopOpen = false;
		_camera = Camera.main.gameObject.GetComponent<CameraController>();
		_pauseGame = GetComponent<PauseGame>();
		_init = false;
	}

	void Update () 
	{
		if(player == true && !_init)
		{
			_spellController = player.GetComponent<SpellController>();
			_towerController = player.GetComponent<TowerController>();
			_characController = player.GetComponent<CharacController>();
			_init = true; 
		}
		if (Input.GetKeyDown(KeyCode.E) && !_pauseGame.pause)
		{
			if (_shopOpen == true)
				Shop(false);
			else
				Shop(true);
		}
	}

	void Shop(bool value)
	{
		Cursor.visible = value;
		guiCanvas.SetActive(!value);
		shopCanvas.SetActive(value);
		shop.SetStatsShop();
		_shopOpen = value;

		//desactive player
		_camera.enabled = !value;
		_spellController.enabled = !value;
		_towerController.enabled = !value;
		_characController.enabled = !value;

		if(value == true)
			Cursor.lockState = CursorLockMode.None;
		else
			Cursor.lockState = CursorLockMode.Locked;
	}
}
