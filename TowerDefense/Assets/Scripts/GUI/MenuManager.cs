using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
    public Menu currentMenu;

	void Start () 
    {
		Time.timeScale = 1;
        ShowMenu(currentMenu);
	}

	public void ShowMenu (Menu menu) 
    {
        if (currentMenu != null)
            currentMenu.IsOpen = false;
        currentMenu = menu;
        currentMenu.IsOpen = true;
	}
}
