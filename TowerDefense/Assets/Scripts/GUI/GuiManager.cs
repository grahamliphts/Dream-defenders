using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour 
{
    public GameObject netPlayer;
	public void LoadGame()
    {
		StartCoroutine("LoadLevel");
		var gameObjects = FindObjectsOfType<GameObject>();
		foreach (var go in gameObjects)
		{
			Debug.Log(go.GetComponent<LevelStart>());
			LevelStart startLevel = go.GetComponent<LevelStart>();
			if (startLevel != null)
				startLevel.OnLoadedLevel(false);
		}
		
    }

	IEnumerator LoadLevel()
	{
		Application.LoadLevel("MainScene");
		yield return null;
	}
}
