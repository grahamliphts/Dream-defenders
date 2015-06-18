using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelStart : MonoBehaviour 
{
	public static LevelStart instance = null;
	public bool modeMulti;

	//Prefab player
    public GameObject netPlayer;
	public GameObject[] netPlayers;
	public GameObject playerSolo;

	//Pools
	public SpellPoolManager[] spellPool;
	public SpellPoolManager currentSpellPool;
	public TowerPoolManager[] towerPool;
	public TowerPoolManager currentTowerPool;

	public GameObject guiManager;
	public Image LifeBar;
	private LifeBarManager _lifeBarPlayer;

	[SerializeField]
	private Transform _spawnPosition;

	//Variables utiles
	public ModelTowerPoolManager modelTowerManager;
	//Debug network
	public bool network;
	private NetworkView _networkView;

	void Start()
	{
		instance = this;
		currentSpellPool = spellPool[0];
		currentTowerPool = towerPool[0];
		_lifeBarPlayer = guiManager.GetComponent<LifeBarManager>();
		_networkView = GetComponent<NetworkView>();
	}

    public void OnLoadedLevel(bool network, int id, int nbPlayers)
    {
		GameObject player;
		Debug.Log("LevelStart: "+id);
		if (network)
		{
			player = netPlayers[id];
			player.GetComponent<CharacController>().isMine = true;
			_networkView.RPC("SetPlayerActive", RPCMode.All, id);
			modeMulti = true;
			/*Enemies Set*/
			_networkView.RPC("AddLeaderEnemies", RPCMode.All, id);
		}
		else
		{
			player = Instantiate(playerSolo, _spawnPosition.position, Quaternion.identity) as GameObject;
			modeMulti = false;
			/*Enemies Set*/
			GetComponent<EnnemyManager>().Players.Add(player.transform);
		}
		
		player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
		Transform targetCamera = null;
		for (int i = 0; i < player.transform.childCount; i++)
		{
			Transform child = player.transform.GetChild(i);
			if (child.name == "TargetCamera")
				targetCamera = child;
		}

		/*Camera Set*/
		if(targetCamera != null)
			Camera.main.gameObject.GetComponent<CameraController>().target = targetCamera;
		Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);

		/*Life Set*/
		_lifeBarPlayer.SetPlayer(player);
		_lifeBarPlayer.SetLifeBar(LifeBar);
		_lifeBarPlayer.SetLifeManager(player.GetComponent<PlayerLifeManager>());

		LoopManager loopManager = GetComponent<LoopManager>();
		loopManager.Player = player;
		loopManager.Init(player.GetComponent<PlayerLifeManager>());
    }

	[RPC]
	private void AddLeaderEnemies(int id)
	{
		GetComponent<EnnemyManager>().Players.Add(netPlayers[id].transform);
	}

	[RPC]
	private void SetPlayerActive(int id)
	{
		netPlayers[id].SetActive(true);
	}
}


