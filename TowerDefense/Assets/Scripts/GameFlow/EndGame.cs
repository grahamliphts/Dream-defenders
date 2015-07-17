using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour 
{
	//Pour la fermeture de la partie
	public RawImage imageClose;
	public Text closeInfo;

	public NexusLife lifeNexus;
	
	private NetworkView _networkView;
	private LoopManager _loopManager;

	private GameObject _player;
	public GameObject player
	{
		set
		{
			_player = value;
		}
		get
		{
			return _player;
		}
	}
	private Stats _stats;
	public Stats stats
	{
		set
		{
			_stats = value;
		}
		get
		{
			return _stats;
		}
	}

	[SerializeField]
	private PauseGame _pauseGame;

	void Start()
	{
		_networkView = GetComponent<NetworkView>();
		_loopManager = GetComponent<LoopManager>();
	}

	void Update () 
	{
		if (_stats == null)
			return;
		if (_stats.life <= 0)
		{
			if (LevelStart.instance.modeMulti)
			{
				NetworkPlayer player = Network.player;
				if(Network.isServer)
				{
					_networkView.RPC("SyncEndGame", RPCMode.Others, "Le serveur est mort");
					StartCoroutine("CloseParty", "Fin de la partie");
				}
				else if(Network.isClient)
					_networkView.RPC("PlayerLeft", RPCMode.All, int.Parse(player.ToString()));
			}
			else
				StartCoroutine("CloseParty", "You died");
		}
		if (lifeNexus.GetLife() <= 0 && Network.isServer)
			_networkView.RPC("SyncEndGame", RPCMode.All, "Nexus has been destroyed");
		else if (lifeNexus.GetLife() <= 0)
			StartCoroutine("CloseParty", "Nexus has been destroyed");
	}

	[RPC]
	private void PlayerLeft(int id)
	{
		LevelStart.instance.netPlayers[id].SetActive(false);
		NetworkPlayer player = Network.player;
		if (int.Parse(player.ToString()) == id)
			StartCoroutine("CloseParty", "You died");
		else
			StartCoroutine("PopupMessage", "Player has left");
	}

	[RPC]
	private void SyncEndGame(string text)
	{
		StartCoroutine("CloseParty", text);
	}

	IEnumerator PopupMessage(string text)
	{
		_pauseGame.Pause(false);
		imageClose.gameObject.SetActive(true);
		closeInfo.text = text;
		yield return new WaitForSeconds(3);
		closeInfo.text = "";
		imageClose.gameObject.SetActive(false);
	}

	public void CloseGame(string text)
	{
		StartCoroutine("CloseParty", text);
	}

	IEnumerator CloseParty(string text)
	{
		if(_pauseGame)
			_pauseGame.Pause(false);
		imageClose.gameObject.SetActive(true);
		closeInfo.text = text;
		_loopManager.lose = true;

		yield return new WaitForSeconds(3);
		player.SetActive(false);

		if (LevelStart.instance.modeMulti)
		{
			if (Network.isServer)
			{
				MasterServer.UnregisterHost();
				Network.Disconnect();
			}
			else
				Network.Disconnect();
		}
		else
		{
			Destroy(GuiManager._instance.gameObject);
			GuiManager._instance = null;
			Destroy(LevelLoader._instance.gameObject);
			LevelLoader._instance = null;
			Application.LoadLevel("MenuScene");
		}
	}

	private void OnDisconnectedFromServer(NetworkDisconnection msg)
	{
		Destroy(GuiManager._instance.gameObject);
		GuiManager._instance = null;
		Destroy(LevelLoader._instance.gameObject);
		LevelLoader._instance = null;
		Application.LoadLevel("MenuScene");
	}

	private void OnPlayerDisconnected(NetworkPlayer player) //Server
	{
		Network.RemoveRPCs(player);
		_networkView.RPC("PlayerLeft", RPCMode.All, int.Parse(player.ToString()));
	}

	//GUI onclick
	public void QuitGame()
	{
		Time.timeScale = 1;
		if (!LevelStart.instance.modeMulti)
		{
			Destroy(GuiManager._instance.gameObject);
			GuiManager._instance = null;
			Destroy(LevelLoader._instance.gameObject);
			LevelLoader._instance = null;
			Application.LoadLevel("MenuScene");
		}
		else if (LevelStart.instance.modeMulti && Network.isServer)
		{
			_networkView.RPC("SyncEndGame", RPCMode.Others, "Le serveur a quitté le jeu");
			StartCoroutine("CloseParty", "Fin de la partie");
		}
			
		else
			Network.Disconnect();
	}
}
