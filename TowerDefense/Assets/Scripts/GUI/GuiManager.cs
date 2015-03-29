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

	IEnumerator LoadLevel()
	{
		Application.LoadLevel("MainScene");
		yield return null;
		var gameObjects = FindObjectsOfType<GameObject>();
		foreach (var go in gameObjects)
		{
			LevelStart startLevel = go.GetComponent<LevelStart>();
			if (startLevel != null)
			{
				Debug.Log("Start level solo");
				startLevel.OnLoadedLevel(false);
			}

		}
	}
}
