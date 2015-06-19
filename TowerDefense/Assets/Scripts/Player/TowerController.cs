﻿using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour 
{
	enum Type {Fire, Elec, Poison, Ice};
	private TowerPoolManager []_towerPool;
	private TowerPoolManager _currentTowerPool;
	private ConstructionController _constructionController;
	private int[] _towerAvailables;

	private NetworkView _networkView;

	private TowerConstructionScript _target;
	private TowerConstructionScript _newTarget;
	private ModelTowerPoolManager _modelTowerManager;

	private int _type;
	private bool _reset;
	private bool _isMine;
	[SerializeField]
	private int _nbtowerAvailables;

	void Start()
	{
		_isMine = GetComponent<CharacController>().isMine;
		_networkView = GetComponent<NetworkView>();
		_towerAvailables = LevelStart.instance.towerAvailables;
		_nbtowerAvailables = _towerAvailables[0];
		Reset();
	}

	void Update()
	{
		Debug.Log(_nbtowerAvailables);
		if ((!LevelStart.instance.modeMulti || _isMine) && LoopManager.modeConstruction)
		{
			if (Input.GetMouseButtonDown(0) && _target.constructionController.GetHitCounter() == 0 && _nbtowerAvailables > 0)
			{
				if (!LevelStart.instance.modeMulti)
					PlaceTower(_constructionController.transform.position, _type);
				else
					_networkView.RPC("SyncTowerPosition", RPCMode.All, _constructionController.transform.position, _type);

				_nbtowerAvailables--;
				_towerAvailables[(int)_type]--;
			}

			if (Input.GetKey("1"))
			{
				ChangeTower(Type.Fire);
				_nbtowerAvailables = _towerAvailables[(int)Type.Fire];
			}
				
			else if (Input.GetKey("2"))
			{
				ChangeTower(Type.Elec);
				_nbtowerAvailables = _towerAvailables[(int)Type.Elec];
			}
			else if (Input.GetKey("3"))
			{
				ChangeTower(Type.Poison);
				_nbtowerAvailables = _towerAvailables[(int)Type.Poison];
			}
				
			else if (Input.GetKey("4"))
			{
				ChangeTower(Type.Ice);
				_nbtowerAvailables = _towerAvailables[(int)Type.Ice];
			}
				

			_constructionController = _target.constructionController;
		}
		else if (LoopManager.modeConstruction == false && _reset == false)
		{
			Reset();
		}
	}

	private void Reset()
	{
		_reset = true;
		_towerPool = LevelStart.instance.towerPool;
		_currentTowerPool = LevelStart.instance.currentTowerPool;
		_modelTowerManager = LevelStart.instance.modelTowerManager;

		_target = _modelTowerManager.GetTower((int)Type.Fire);
		_target.gameObject.SetActive(true);
		_target.constructionController.enabled = true;

		_type = 0;
	}

	void ChangeTower(Type type)
	{
		_type = (int)type;
		_currentTowerPool = _towerPool[(int)type];
		//Debug.Log("avant if " + _target == null ? "NULL" : _target.name);
		if (_target != _modelTowerManager.GetTower((int)type))
		{
			//Debug.Log("if");
			_newTarget = _modelTowerManager.GetTower((int)type);
			SetTower(_target, _newTarget);
			_target = _newTarget;
		}
		_reset = false;
	}

	[RPC]
	void SyncTowerPosition(Vector3 position, int type)
	{
		PlaceTower(position, type);
	}

	public void PlaceTower(Vector3 position, int type)
	{
		var tower = _towerPool[type].GetTower();
		if (tower != null)
		{
			tower.gameObject.SetActive(true);
			tower.newtransform.position = position;
			//add box collider for shoot range
			tower.RangeCollider.enabled = true;
			tower.RangeCollider.isTrigger = true;
			//add box collider to tower
			tower.OwnCollider.enabled = true;
		}
		
		
	}

	void SetTower(TowerConstructionScript previousTower, TowerConstructionScript tower)
	{
		previousTower.gameObject.SetActive(false);
		previousTower.constructionController.enabled = false;
		previousTower.Transform.position = new Vector3(1000, 1000, 1000);

		tower.gameObject.SetActive(true);
		tower.constructionController.enabled = true;
		tower.constructionController.Reset();
		tower.constructionController.SetConstruction(true);
	}
}

