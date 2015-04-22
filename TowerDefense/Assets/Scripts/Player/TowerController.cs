﻿using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour 
{
	private TowerPoolManager []_towerPool;
	private TowerPoolManager _currentTowerPool;
	private ConstructionController _constructionController;
	private NetworkView _networkView;

	private TowerConstructionScript _target;
	private TowerConstructionScript _newTarget;
	private ModelTowerPoolManager _modelTowerManager;

	void Start()
	{
		_networkView = GetComponent<NetworkView>();
		_towerPool = LevelStart.instance.towerPool;
		_currentTowerPool = LevelStart.instance.currentTowerPool;
		_modelTowerManager = LevelStart.instance.modelTowerManager;

		_target = _modelTowerManager.GetFireTower();
		_target.gameObject.SetActive(true);
		_target.constructionController.enabled = true;

	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && _target.constructionController.GetHitCounter() == 0 && LoopManager.modeConstruction
			&& (LevelStart.instance.modeMulti == false || _networkView.isMine))
		{
			if (LevelStart.instance.modeMulti == false)
				PlaceTower(_constructionController.transform.position);
			else
				_networkView.RPC("SyncTowerPosition", RPCMode.All, _constructionController.transform.position);
		}

		if (Input.GetKey("1"))
		{
			_currentTowerPool = _towerPool[0];
			if (_target != _modelTowerManager.GetFireTower())
			{
				_newTarget = _modelTowerManager.GetFireTower();
				SetTower(_target, _newTarget);
				_target = _newTarget;
				
			}
		}
		
		else if (Input.GetKey("2"))
		{
			_currentTowerPool = _towerPool[1];
			if (_target != _modelTowerManager.GetElectricTower())
			{
				_newTarget = _modelTowerManager.GetElectricTower();
				SetTower(_target, _newTarget);
				_target = _newTarget;
			}
		}

		_constructionController = _target.constructionController;
	}

	[RPC]
	void SyncTowerPosition(Vector3 position)
	{
		PlaceTower(position);
	}

	public void PlaceTower(Vector3 position)
	{
		var tower = _currentTowerPool.GetTower();
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

