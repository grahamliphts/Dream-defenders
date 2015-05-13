using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour 
{
	void Start()
	{
		DontDestroyOnLoad(this);
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
