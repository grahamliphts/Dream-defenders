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
    private uint _playerCount;
    private int _nbPlayersMax;
	public MenuManager _menuManager;

    void Start()
    {
        _players = new List<NetworkPlayer>();
        _playerCount = 1;
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
		 int _nbPlayersMax, i;
		 if (!int.TryParse(nbPlayersInput.text, out _nbPlayersMax))
			 return;
		 else
		 {
			 Debug.Log("Success");
			 if (serverName.text != "" && nbPlayersInput.text != "")
			 {
				 _nbPlayersMax = int.Parse(nbPlayersInput.text) - 1;
				 Network.InitializeServer(_nbPlayersMax, _listenPort, !Network.HavePublicAddress());
				 MasterServer.RegisterHost(_typeName, serverName.text, "player" + serverName.text);
				 _menuManager.ShowMenu(LobbyMenu);
			 }
		 }
       
	}

    public void RefreshPlayersCount()
    {
        if(Network.isServer)
        {
            Debug.Log("Server:" + _playerCount);
            nbPlayersConnected.text = _playerCount + "/" + (_nbPlayersMax + 1);
        }
        else if(Network.isClient)
        {
            Debug.Log("Client:"+_playerCount);
            nbPlayersConnected.text = _playerCount + "/" + _hostConnected.playerLimit;
            Debug.Log(_hostConnected.connectedPlayers);
        }
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
				button.GetComponentInChildren<Text>().text = _hostList[i].gameName + "-" + _hostList[i].connectedPlayers.ToString() + "/" + _hostList[i].playerLimit.ToString();

                HostData host = _hostList[i];
				button.GetComponent<Button>().onClick.AddListener(() => 
                {
                    MenuManager menuManager = canvas.GetComponent<MenuManager>();
                    menuManager.ShowMenu(LobbyMenu);
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
        Debug.Log("On player connected:"+_playerCount);
	}

    private void OnConnectedToServer()  //Client 
    {
        _playerCount++;
		Debug.Log("On connected to server:" + _playerCount);
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
