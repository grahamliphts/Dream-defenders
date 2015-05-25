﻿using UnityEngine;
using System.Collections;

public class MonsterLifeManager : MonoBehaviour 
{
    private float _life;
	[SerializeField]
	private float _lifeMax;

    public string[] _tag;
    public int[] _damage;
    public Material materialModel;
	private Material _matHeathBar;
    public GameObject healthBar;

	private NetworkView _networkView;

    void Start()
    {
		healthBar.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialModel);
		_matHeathBar = healthBar.GetComponent<Renderer>().material;
		_matHeathBar.SetFloat("_HP", _lifeMax);
		_life = _lifeMax;
		_networkView = GetComponent<NetworkView>();
    }

    void OnCollisionEnter(Collision col)
    {
		if(LevelStart.instance.modeMulti == false || Network.isServer)
		{
			int count = 0;
			foreach (string element in _tag)
			{
				if (col.gameObject.tag == element)
					_life = _life - _damage[count];
				count++;
			}
			if (col.gameObject.tag == "proj_friend")
				_life = _life - 2;

			if (LevelStart.instance.modeMulti)
			{
				_networkView.RPC("SyncLifeEnemy", RPCMode.All, _life);
				if (_life <= 0)
				{
					transform.position = new Vector3(1000, 1000, 1000);
					gameObject.SetActive(false);
				}
			}
			else
			{
				_matHeathBar.SetFloat("_HP", _life);
				SetColorLife(_life);

				if (_life <= 0)
				{
					transform.position = new Vector3(1000, 1000, 1000);
					gameObject.SetActive(false);
				}
			}
		}
    }

	[RPC]
	private void SyncLifeEnemy(float life)
	{
		_life = life;

		_matHeathBar.SetFloat("_HP", _life);
		SetColorLife(_life);

		if (_life <= 0 && Network.isClient)
		{
			transform.position = new Vector3(1000, 1000, 1000);
			gameObject.SetActive(false);
		}

	}
	private void SetColorLife(float life)
	{
		if(life <= _lifeMax/2 && life >= _lifeMax/4)
			_matHeathBar.SetColor("_Color", Color.yellow);
		if (life <= _lifeMax / 4)
			_matHeathBar.SetColor("_Color", Color.red);
	}
}
