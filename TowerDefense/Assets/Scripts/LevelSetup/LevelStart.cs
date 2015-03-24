﻿using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour 
{
    public GameObject netPlayer;
	public SpellPoolManager spellPool;
	private int _countPlayer;

	void Start()
	{
		_countPlayer = 0;
	}
    void OnNetworkLoadedLevel()
    {
        Debug.Log("Level was loaded");
        GameObject player = Network.Instantiate(netPlayer, new Vector3(5, 1, 5), Quaternion.identity, 0) as GameObject;

		Transform targetCamera = null;
		Transform firePoint = null;
		for (int i = 0; i < player.transform.childCount; i++)
		{
			Transform child = player.transform.GetChild(i);
			if (child.name == "TargetCamera")
				targetCamera = child;
			else if (child.name == "Fire_point")
				firePoint = child;
		}

		if(targetCamera != null)
			Camera.main.gameObject.GetComponent<CameraController>().target = targetCamera;

		Debug.Log(_countPlayer);
		GetComponent<EnnemyManager>().players.Add(player.transform);
		//Debug.Log(firePoint);
		firePoint.GetComponent<SpellController>()._spellPoolManager = spellPool;

		LoopManager loopManager = GetComponent<LoopManager>();
		loopManager.Player = player;
		loopManager.Init();
    }
}
