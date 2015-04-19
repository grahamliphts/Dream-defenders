using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour 
{
	public static LevelStart instance = null;
	public bool modeMulti;
    public GameObject netPlayer;
	public GameObject playerSolo;
	public SpellPoolManager[] spellPool;
	public SpellPoolManager currentSpellPool;

	public GameObject lifeBar;
	public ConstructionController constructionController;
	public TowerPoolManager[] towerPool;
	public TowerPoolManager currentTowerPool;

	public ModelTowerPoolManager modelTowerManager;

	[SerializeField]
	private Transform _spawnPosition;

	public bool network;

	void Start()
	{
		instance = this;
		currentSpellPool = spellPool[0];
		currentTowerPool = towerPool[0];
	}

    public void OnLoadedLevel(bool network)
    {
		GameObject player;
		if (network)
		{
			player = Network.Instantiate(netPlayer, _spawnPosition.position, Quaternion.identity, 1) as GameObject;
			modeMulti = true;
		}
		else
		{
			player = Instantiate(playerSolo, _spawnPosition.position, Quaternion.identity) as GameObject;
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
