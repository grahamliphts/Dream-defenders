using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour 
{
    public GameObject netPlayer;
	public void LoadGame()
    {
        Application.LoadLevel("MainScene"); 
    }
}
