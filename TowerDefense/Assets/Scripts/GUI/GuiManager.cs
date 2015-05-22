using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour 
{
	void Start()
	{
		DontDestroyOnLoad(this);
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
