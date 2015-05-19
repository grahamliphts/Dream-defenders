﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiInGameManager : MonoBehaviour 
{
	public Shadow action1;
	public Shadow action2;
	public Shadow action3;
	public Shadow action4;

	private Shadow _shadowTarget;

	private NexusLife nexusLife;
	void Start () 
    {
		_shadowTarget = action1;
		/* _nbFireTower = ElectricPool.GetTowerNumberMax();
		_nbElectricTower = FirePool.GetTowerNumberMax();*/
		//ValueElectricTower.text = (_nbElectricTower - ElectricPool.GetTowersNumber()).ToString();
		//ValueFireTower.text = (_nbFireTower - FirePool.GetTowersNumber()).ToString();
	}
	
	void Update () 
    {
		if (Input.GetKey("1"))
			SetColor(_shadowTarget, action1);
		else if (Input.GetKey("2"))
			SetColor(_shadowTarget, action2);
		else if (Input.GetKey("3"))
			SetColor(_shadowTarget, action3);
		else if (Input.GetKey("4"))
			SetColor(_shadowTarget, action4);

		//int life = nexusLife.GetLifeNexus();
	}

	void SetColor(Shadow previous, Shadow current)
	{
		previous.effectColor = Color.black;
		current.effectColor = Color.yellow;

		_shadowTarget = current;
	}
}