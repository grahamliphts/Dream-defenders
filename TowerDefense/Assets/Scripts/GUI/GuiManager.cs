using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour 
{
	private static GuiManager _instance;

	void Start()
	{
		if (!_instance)
			_instance = this;
		else
			Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject);
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
		LevelStart.instance.OnLoadedLevel(false);
	}
}
