using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuiManager : MonoBehaviour 
{
	public static GuiManager _instance;

	void Start()
	{
		if (!_instance)
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void LoadGame()
    {
		StartCoroutine("LoadLevel");
    }

	public void QuitGame()
	{
		Application.Quit();
	}

	IEnumerator LoadLevel()
	{
		Application.LoadLevel("MainScene");
		yield return null;
		LevelStart.instance.OnLoadedLevel(false, 0, 1);
	}
}
