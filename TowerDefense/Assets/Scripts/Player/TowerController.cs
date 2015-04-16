using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour 
{
	private TowerPoolManager _towerPoolManager;
	private ConstructionController _constructionController;
	private NetworkView _networkView;

	void Start()
	{
		_networkView = GetComponent<NetworkView>();
		_towerPoolManager = LevelStart.instance.towerPoolManager;
	}
	[RPC]
	void SyncTowerPosition()
	{
		Debug.Log("Sync tower position");
		PlaceTower();
	}

	public void PlaceTower()
	{
		Vector3 position = Vector3.zero;
		Debug.Log("Place Tower");

			//Debug.Log("NetworkView");
			var tower = _towerPoolManager.GetTower();
			if (tower != null)
			{
				tower.gameObject.SetActive(true);
				if (LevelStart.instance.modeMulti == false || _networkView.isMine)
				{
					position= _constructionController.transform.position;
				}
				Debug.Log(position);
				tower.newtransform.position = position;
				//add box collider for shoot range
				tower.RangeCollider.enabled = true;
				tower.RangeCollider.isTrigger = true;
				//add box collider to tower
				tower.OwnCollider.enabled = true;
			}
		
		
	}

	public void SetConstructionController(ConstructionController constructionController)
	{
		_constructionController = constructionController;
	}
}
