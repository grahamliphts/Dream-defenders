using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour 
{
    public GameObject netPlayer;
	public SpellPoolManager spellPool;
	public GameObject lifeBar;

	[SerializeField]
	private Transform _spawnPosition;

	public bool network;

	void Start()
	{
		if (Application.isEditor)
		{
			//OnLoadedLevel(network);
		}
	}

    public void OnLoadedLevel(bool network)
    {
        Debug.Log("Level was loaded");
		GameObject player;
		if (network)
			player = Network.Instantiate(netPlayer, _spawnPosition.position, Quaternion.identity, 1) as GameObject;
		else
		{
			player = Instantiate(netPlayer, _spawnPosition.position, Quaternion.identity) as GameObject;
			Destroy(player.GetComponent<NetworkView>());
		}

		player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
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

		/*Camera Set*/
		if(targetCamera != null)
			Camera.main.gameObject.GetComponent<CameraController>().target = targetCamera;
		Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);

		/*Enemies Set*/
		GetComponent<EnnemyManager>().players.Add(player.transform);

		/*Pool Set*/
		firePoint.GetComponent<SpellController>().SetSpellPoolManager(spellPool);

		/*Life Set*/
		LifeBarManager lifeBarManager = lifeBar.GetComponent<LifeBarManager>();
		lifeBarManager.Player = player;
		lifeBarManager.SetLifeBar(lifeBar.transform.GetChild(0).GetComponent<Image>());
		lifeBarManager.SetLifeManager(player.GetComponent<PlayerLifeManager>());

		LoopManager loopManager = GetComponent<LoopManager>();
		loopManager.Player = player;
		loopManager.Init(firePoint.GetComponent<SpellController>(), player.GetComponent<PlayerLifeManager>());
    }
}
