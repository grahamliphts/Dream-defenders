using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour 
{
	public static LevelStart instance = null;
	public bool modeMulti;
    public GameObject netPlayer;
	public GameObject playerSolo;
	public SpellPoolManager spellPool;
	public GameObject lifeBar;
	public ConstructionController constructionController;
	public TowerPoolManager towerPoolManager;

	[SerializeField]
	private Transform _spawnPosition;

	public bool network;

	void Start()
	{
		instance = this;
	}

    public void OnLoadedLevel(bool network)
    {
        Debug.Log("Level was loaded");
		GameObject player;
		if (network)
		{
			player = Network.Instantiate(netPlayer, _spawnPosition.position, Quaternion.identity, 1) as GameObject;
			constructionController.SetNetworkView(player.GetComponent<NetworkView>());
			modeMulti = true;
		}
		else
		{
			player = Instantiate(playerSolo, _spawnPosition.position, Quaternion.identity) as GameObject;
			constructionController.SetTowerController(player.GetComponent<TowerController>());
			modeMulti = false;
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

		//player.GetComponent<TowerController>().SetTowerPoolManager(towerPoolManager);
		player.GetComponent<TowerController>().SetConstructionController(constructionController);
		

		/*Enemies Set*/
		GetComponent<EnnemyManager>().players.Add(player.transform);

		/*Life Set*/
		LifeBarManager lifeBarManager = lifeBar.GetComponent<LifeBarManager>();
		lifeBarManager.Player = player;
		lifeBarManager.SetLifeBar(lifeBar.transform.GetChild(0).GetComponent<Image>());
		lifeBarManager.SetLifeManager(player.GetComponent<PlayerLifeManager>());

		LoopManager loopManager = GetComponent<LoopManager>();
		loopManager.Player = player;
		loopManager.Init(player.GetComponent<PlayerLifeManager>());
    }
}
