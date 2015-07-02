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
		StartCoroutine("LoadLevel", "MainScene");
    }

	public void LoadTuto()
	{
		StartCoroutine("LoadLevel", "TutoScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	IEnumerator LoadLevel(string nameScene)
	{
		Application.LoadLevel(nameScene);
		yield return null;
		if(nameScene == "TutoScene")
			LevelStart.instance.OnLoadedLevel("Tuto", 0);
		else
			LevelStart.instance.OnLoadedLevel("Solo", 0);
	}
}
