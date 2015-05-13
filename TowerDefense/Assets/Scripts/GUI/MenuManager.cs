﻿using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour 
{
    public Menu currentMenu;

	void Start () 
    {
        ShowMenu(currentMenu);
	}

	public void ShowMenu (Menu menu) 
    {
		Debug.Log("Show Menu");
        if (currentMenu != null)
            currentMenu.IsOpen = false;
        currentMenu = menu;
        currentMenu.IsOpen = true;
	}
}
