using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelStart : MonoBehaviour 
{
	public static LevelStart instance = null;
	public bool modeMulti;
	public bool modeTuto;

	//Players
	public GameObject[] netPlayers;
	public GameObject playerSolo;

	//Pools Spell et Tower
	public SpellPoolManager[] spellPool;
	public SpellPoolManager currentSpellPool;
	public TowerPoolManager[] towerPool;

	//Availables Towers
	public int[] towerAvailables;

	//LifeBarSet
	public GameObject guiManager;
	private LifeBarManager _lifeBarPlayer;

	//ManaBar
	private ManaBarManager _manaBar;

	[SerializeField]
	private Transform _spawnPosition;

	//Variables utiles
	public ModelTowerPoolManager modelTowerManager;

	//Debug network
	public bool network;
	private NetworkView _networkView;

	//Components
	private CameraController _camera;
	private EnnemyManager _enemiesManager;
	private LoopManager _loopManager;
	private EndGame _endGame;

	void Start()
	{
		instance = this;
		modeTuto = false;
		currentSpellPool = spellPool[0];
		_lifeBarPlayer = guiManager.GetComponent<LifeBarManager>();
		_manaBar = guiManager.GetComponent<ManaBarManager>();
		_networkView = GetComponent<NetworkView>();
		_camera = Camera.main.gameObject.GetComponent<CameraController>();
		_enemiesManager = GetComponent<EnnemyManager>();
		_loopManager = GetComponent<LoopManager>();
		_endGame = GetComponent<EndGame>();
	}

    public void OnLoadedLevel(string mode, int id)
    {
		GameObject player;
		if (mode == "Reseau")
		{
			player = netPlayers[id];
			player.GetComponent<CharacController>().isMine = true;
			var viewID1 = Network.AllocateViewID();
			var viewID2 = Network.AllocateViewID();
			_networkView.RPC("SetPlayerActive", RPCMode.All, id, viewID1, viewID2);
			modeMulti = true;
			/*Enemies Set*/
			_networkView.RPC("AddLeaderEnemies", RPCMode.All, id);
		}
		else if (mode == "Solo")
		{
			player = playerSolo;
			player.SetActive(true);
			modeMulti = false;
			/*Enemies Set*/
			_enemiesManager.players.Add(player.transform);
		}

		else
		{
			modeTuto = true;
			player = playerSolo;
			player.SetActive(true);
			modeMulti = false;
		}
		
		Transform targetCamera = null;
		for (int i = 0; i < player.transform.childCount; i++)
		{
			Transform child = player.transform.GetChild(i);
			if (child.name == "TargetCamera")
				targetCamera = child;
		}
		
		/*Camera Set*/
		_camera.target = targetCamera;
		_camera.player = player;

		/*Life Set*/
		Stats stats = player.GetComponent<Stats>();
		_lifeBarPlayer.player = player;
		_lifeBarPlayer.stats = stats;
		_manaBar.player = player;
		_manaBar.stats = stats;

		_endGame.player = player;
		_endGame.stats = stats;
		if (mode != "Tuto")
			_loopManager.Init();
    }

	[RPC]
	private void AddLeaderEnemies(int id)
	{
		_enemiesManager.players.Add(netPlayers[id].transform);
	}

	[RPC]
	private void SetPlayerActive(int id, NetworkViewID viewID1, NetworkViewID viewID2)
	{
		NetworkView []nView = netPlayers[id].gameObject.GetComponents<NetworkView>();
		nView[0].viewID = viewID1;
		nView[1].viewID = viewID2;
		netPlayers[id].SetActive(true);
	}
}

