﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour 
{
    //Gui
    public GameObject serverList;
    public Text serverName;
    public Text nbPlayersInput;
    public Canvas canvas;
    public Menu LobbyMenu;
	public Menu ServerMenu;
    public Text nbPlayersConnected;

	[SerializeField]
	GameObject modelButton;

    //Network
	private int _listenPort = 4242; //le port d'écoute du serveur
    private const string _typeName = "TowerDefense";
    private string _gameName;
    private HostData[] _hostList;
    List<NetworkPlayer> _players;
    private HostData _hostConnected;
	private int _playerCount;
	private int _nbPlayersMax;
	public MenuManager _menuManager;
	public LevelLoader LevelLoader;
	private bool _register;

    void Start()
    {
        _players = new List<NetworkPlayer>();
        _playerCount = 0;
		_register = false;
		_menuManager = canvas.GetComponent<MenuManager>();
		StartCoroutine("RefreshHostList");
    }

	void Awake()
	{
        MasterServer.ipAddress = "gp-raveh.com";
        MasterServer.port = 23466;
        Network.natFacilitatorIP = "gp-raveh.com";
        Network.natFacilitatorPort = 24466;
	}

    public void QuitLobby()
    {
		_playerCount = 0;
		Network.Disconnect();
		if (Network.isServer)
			MasterServer.UnregisterHost();
		LevelLoader.SetPlayerCount(_playerCount);
    }

	public void StartServer() 
	{
		 int _nbPlayersMax;
		 if (!int.TryParse(nbPlayersInput.text, out _nbPlayersMax))
			 return;
		 else
		 {
			 _register = true;
			 Debug.Log("Success");
			 if (serverName.text != "" && nbPlayersInput.text != "")
			 {
				 _nbPlayersMax = int.Parse(nbPlayersInput.text);
				 _playerCount++;
				 LevelLoader.SetPlayerMax(_nbPlayersMax);
				 LevelLoader.SetPlayerCount(_playerCount);
				 Network.InitializeServer(_nbPlayersMax, _listenPort, !Network.HavePublicAddress());
				 MasterServer.RegisterHost(_typeName, serverName.text, "player" + serverName.text);

				 _menuManager.ShowMenu(LobbyMenu);
			 }
		 }
       
	}

	IEnumerator RefreshHostList()
	{
		while (true)
		{
			nbPlayersConnected.text = LevelLoader.GetPlayerCount() + "/" + LevelLoader.GetPlayerMax();
			for (int i = 0; i < serverList.transform.childCount; i++)
				Destroy(serverList.transform.GetChild(i).gameObject);
			if(_register == false)
				MasterServer.RequestHostList(_typeName);
			if (_hostList != null)
			{
				Debug.Log("Nb host" + _hostList.Length);
				for (int i = 0; i < _hostList.Length; i++)
				{
					RectTransform rectTransform;

					GameObject button = Instantiate(modelButton) as GameObject;
					button.transform.SetParent(serverList.transform);

					rectTransform = button.GetComponent<RectTransform>();
					rectTransform.localScale = new Vector3(1, 1, 1);
					button.GetComponentInChildren<Text>().text = _hostList[i].gameName + " - " + _hostList[i].connectedPlayers.ToString() + "/" + (_hostList[i].playerLimit - 1).ToString();

					HostData host = _hostList[i];
					button.GetComponent<Button>().onClick.AddListener(() =>
					{
						_menuManager.ShowMenu(LobbyMenu);
						JoinServer(host);
					});
				}
				MasterServer.ClearHostList();
				_hostList = null;
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
    public void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
        _hostConnected = hostData;
		_playerCount = 1;
		LevelLoader.SetPlayerCount(_playerCount);
    }


    private void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            _hostList = MasterServer.PollHostList();
			if (_hostList.Length == 0)
				_hostList = null;
        }
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
			_register = false;
        Debug.Log(msEvent.ToString());
    }

    private void OnPlayerConnected(NetworkPlayer player) //Server
	{
        _players.Add(player); 
        _playerCount++;
		LevelLoader.SetPlayerCount(_playerCount);
	}

    private void OnConnectedToServer()  //Client 
    {
        _playerCount++;
		LevelLoader.SetPlayerMax(_hostConnected.playerLimit - 1);
		LevelLoader.SetPlayerCount(_playerCount);
    }

	private void OnPlayerDisconnected(NetworkPlayer player) //Server
	{
		_playerCount--;
		LevelLoader.SetPlayerCount(_playerCount);
		Debug.Log("Clean up after player " +  player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}

	private void OnDisconnectedFromServer(NetworkDisconnection msg)
	{
		_playerCount = 0;
		LevelLoader.SetPlayerCount(_playerCount);
		_menuManager.ShowMenu(ServerMenu);
	}

	//Debug;

	private void OnServerInitialized()
	{
		Debug.Log("Server initialized and ready");
	}

    private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to master server: " + error);
    }

    private void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
    }
}
