using UnityEngine;
using System.Collections;

public class OpenShop : MonoBehaviour 
{
	public GameObject guiCanvas;
	public GameObject shopCanvas;
	public ShopManager shop;
	private bool _shop;
	private CameraController _camera;
	public GameObject player;
	//todo charccontroller + spell controller + tower controller enabled

	void Start()
	{
		_shop = false;
		_camera = Camera.main.gameObject.GetComponent<CameraController>();
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			if (_shop == true)
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
		_shop = value;
		_camera.enabled = !value;
		if(value == true)
			Cursor.lockState = CursorLockMode.None;
		else
			Cursor.lockState = CursorLockMode.Locked;
	}
}
