﻿using UnityEngine;
using System.Collections;

public class NexusLife : MonoBehaviour {

	[SerializeField]
	private float _life;
	[SerializeField]
	private int damage;

	private NetworkView _networkView;

	void Start()
	{
		_life = 100;
		_networkView = GetComponent<NetworkView>();
	}

	void OnCollisionEnter(Collision col)
	{
		if(!LevelStart.instance.modeMulti || Network.isServer)
		{
			if (_life > 0)
			{
				if (LevelStart.instance.modeMulti)
					_networkView.RPC("SyncLifeNexus", RPCMode.All, (_life - damage));
				else
					_life = _life - damage;
			}
		}
	}

	[RPC]
	private void SyncLifeNexus(float life)
	{
		_life = life;
	}

	public float GetLife()
	{
		return _life;
	}
}