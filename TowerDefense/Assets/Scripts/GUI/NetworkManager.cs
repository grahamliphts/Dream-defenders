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
    public Button modelButton;
    public Text nbPlayersConnected;

    //Network
	private int _listenPort = 4242; //le port d'écoute du serveur
    private const string _typeName = "TowerDefense";
    private string _gameName;
    private HostData[] _hostList;
    List<NetworkPlayer> _players;
    private HostData _hostConnected;
    private uint _playerCount;
    private string _serverName;
    private int _nbPlayersMax;

    void Start()
    {
        _players = new List<NetworkPlayer>();
        _playerCount = 1;
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
        MasterServer.UnregisterHost();
    }

	public void StartServer () 
	{
        if (serverName.text != "" && nbPlayersInput.text != "")
        {
            Network.InitializeServer(int.Parse(nbPlayersInput.text) - 1, _listenPort, !Network.HavePublicAddress());
            MasterServer.RegisterHost(_typeName, serverName.text, "player" + serverName.text);
            _nbPlayersMax = int.Parse(nbPlayersInput.text);
            _serverName = serverName.text;
        }
	}

    public void RefreshPlayersName()
    {
        if(Network.isServer)
        {
            Debug.Log("Server:" + _playerCount);
            nbPlayersConnected.text = _playerCount + "/" + _nbPlayersMax;
            

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
                float anchorMinX = 0.1f;
                float anchorMaxX = 0.8f;

                Button b_object;
                b_object = Instantiate(modelButton) as Button;
                b_object.transform.parent = serverList.transform;
                rectTransform = b_object.GetComponent<RectTransform>();
                rectTransform.anchorMax = new Vector2(anchorMaxX,  1.0f - 0.1f*(i + 1));
                rectTransform.anchorMin = new Vector2(anchorMinX, 1.0f - 0.1f*(i + 2));
                rectTransform.sizeDelta = new Vector2(130, 50);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                b_object.GetComponentInChildren<Text>().text = _hostList[i].gameName + "-" + _hostList[i].connectedPlayers.ToString() + "/" + _hostList[i].playerLimit.ToString();

                HostData host = _hostList[i];
                b_object.onClick.RemoveAllListeners();
                b_object.onClick.AddListener( () => 
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
        Debug.Log(_playerCount);
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
