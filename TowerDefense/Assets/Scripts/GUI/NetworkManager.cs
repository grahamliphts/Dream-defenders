using UnityEngine;
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

    void Start()
    {
        _players = new List<NetworkPlayer>();
        _playerCount = 0;
		_menuManager = canvas.GetComponent<MenuManager>();
    }

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
        MasterServer.ipAddress = "gp-raveh.com";
        MasterServer.port = 23466;
        Network.natFacilitatorIP = "gp-raveh.com";
        Network.natFacilitatorPort = 24466;
	}

    public void ShutDownServer()
    {
		Debug.Log("Unregister");
		if (Network.isServer)
		{
			Network.Disconnect();
			MasterServer.UnregisterHost();
		}
    }

	public void StartServer () 
	{
		 int _nbPlayersMax;
		 if (!int.TryParse(nbPlayersInput.text, out _nbPlayersMax))
			 return;
		 else
		 {
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

    public void RefreshPlayersCount()
    {
		nbPlayersConnected.text = LevelLoader.GetPlayerCount() + "/" + LevelLoader.GetPlayerMax();
    }

    public void RefreshHostList()
    {
        for (int i = 0; i < serverList.transform.childCount; i++)
           Destroy(serverList.transform.GetChild(i).gameObject);
        MasterServer.RequestHostList(_typeName);
    }

    public void Update()
    {
        if (_hostList != null)
        {
            Debug.Log("Nb Host:" + _hostList.Length);
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
            _hostList = null;
        }
    }

    public void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
        _hostConnected = hostData;
		_playerCount = 1;
    }


    private void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            _hostList = MasterServer.PollHostList();
            
        }
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

	private void OnDisconnectedFromServer(NetworkDisconnection msg)
	{
		Debug.Log("OnDisconnectedFromServer");
		Application.LoadLevel("MenuScene");
	}

	//Debug;
    private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to master server: " + error);
    }

    private void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
    }
}
