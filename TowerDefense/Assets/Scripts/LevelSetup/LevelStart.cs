using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour 
{
    public GameObject netPlayer;
	public SpellPoolManager spellPool;
	private int _countPlayer;

	[SerializeField]
	private Transform _spawnPosition;

	public bool network;

	void Start()
	{
		_countPlayer = 0;
		if (Application.isEditor)
		{
			OnLoadedLevel(network);
		}
	}

    public void OnLoadedLevel(bool network)
    {
        Debug.Log("Level was loaded");
		GameObject player;
		if (network)
			player = Network.Instantiate(netPlayer, _spawnPosition.position, Quaternion.identity, 0) as GameObject;
		else
			player = Instantiate(netPlayer, _spawnPosition.position, Quaternion.identity) as GameObject;
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

		if(targetCamera != null)
			Camera.main.gameObject.GetComponent<CameraController>().target = targetCamera;

		Debug.Log("Count player:"+_countPlayer);
		GetComponent<EnnemyManager>().players.Add(player.transform);
		firePoint.GetComponent<SpellController>()._spellPoolManager = spellPool;
		LoopManager loopManager = GetComponent<LoopManager>();
		loopManager.Player = player;
		loopManager.Init();
    }
}
