﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NbFloorScript : MonoBehaviour 
{
	public Text nbFloorValue;
	[SerializeField]
	private PointManager _pointManager;
	
	void Update () 
	{
		int value = _pointManager.GetTeleport().GetNbFloor();
		if(LevelStart.instance.modeMulti)
			nbFloorValue.text = "";
		else
			nbFloorValue.text = "Etage " + (value+1).ToString();
	}

	public void SetPointManager(PointManager pointManager)
	{
		_pointManager = pointManager;
	}
}
