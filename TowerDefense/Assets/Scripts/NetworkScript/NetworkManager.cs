using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour 
{
    //Gui
    public GameObject serverList;
	public Text ipInput;
    public Text serverName;
    public Text nbPlayersInput;
    public Menu LobbyMenu;
	public Menu ServerMenu;
    public Text nbPlayersConnected;
	public Text ipAdress;
	public Toggle local;

	[SerializeField]
	GameObject modelButton;

    //Network
	private int _listenPort = 4242; //le port d'écoute du serveur
    private const string _typeName = "TowerDefense";
    private string _gameName;
    private HostData[] _hostList;

	private int _nbPlayersMax;
	public MenuManager MenuManager;
	public LevelLoader LevelLoader;
	private bool _register;

    void Start()
    {
		_register = false;
		StartCoroutine("RefreshHostList");
    }

	void Awake()
	{
		/*MasterServer.ipAddress = "masterserver.greenkumquat.com";
		MasterServer.port = 53;
		Network.natFacilitatorIP = "masterserver.greenkumquat.com";
		Network.natFacilitatorPort = 67;*/
        MasterServer.ipAddress = "gp-raveh.com";
        MasterServer.port = 23466;
        Network.natFacilitatorIP = "gp-raveh.com";
        Network.natFacilitatorPort = 24466;
	}

    public void QuitLobby()
    {
		Network.Disconnect();
		if (Network.isServer)
			MasterServer.UnregisterHost();
		LevelLoader.nbPlayerCount = 0;
    }

	public void StartServer() 
	{
		 int _nbPlayersMax;
		 if (!int.TryParse(nbPlayersInput.text, out _nbPlayersMax))
			 return;
		 else
		 {
			 if (serverName.text != "" && nbPlayersInput.text != "" && _nbPlayersMax >= 1 && _nbPlayersMax <= 4)
			 {
				 _nbPlayersMax = int.Parse(nbPlayersInput.text);
				 LevelLoader.nbPlayerMax = _nbPlayersMax;
				 LevelLoader.nbPlayerCount = 1;
				 if (!local.isOn)
				 {
					 Network.InitializeServer(_nbPlayersMax, _listenPort, !Network.HavePublicAddress());
					 MasterServer.RegisterHost(_typeName, serverName.text, "player" + serverName.text);
				 }
				 else
					 Network.InitializeServer(_nbPlayersMax, _listenPort, Network.HavePublicAddress());
				 MenuManager.ShowMenu(LobbyMenu);
			 }
		 }
	}

	IEnumerator RefreshHostList()
	{
		while (true)
		{
			nbPlayersConnected.text = LevelLoader.nbPlayerCount+ "/" + LevelLoader.nbPlayerMax;
			for (int i = 0; i < serverList.transform.childCount; i++)
				Destroy(serverList.transform.GetChild(i).gameObject);
			if(!_register)
				MasterServer.RequestHostList(_typeName);
				
			if (_hostList != null)
			{
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
						MenuManager.ShowMenu(LobbyMenu);
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
    }

	public void JoinLocal()
	{
		if(ipInput.text != "")
		{
			Network.Connect(ipInput.text, _listenPort);
			MenuManager.ShowMenu(LobbyMenu);
		}
	}

	private void OnPlayerConnected(NetworkPlayer player) //Server
	{
		int nbConnections = Network.connections.Length;
		LevelLoader.nbPlayerCount = nbConnections + 1;
		LevelLoader.nbPlayerMax = Network.maxConnections;
	}

	private void OnPlayerDisconnected(NetworkPlayer player) //Server
	{
		int nbConnections = Network.connections.Length;
		LevelLoader.nbPlayerCount = nbConnections + 1;
		LevelLoader.nbPlayerMax = Network.maxConnections;
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		int nb = LevelLoader.nbPlayerCount;
		int nbMax = LevelLoader.nbPlayerMax;
		if (stream.isWriting)
		{
			nb = LevelLoader.nbPlayerCount;
			nbMax = LevelLoader.nbPlayerMax;
			stream.Serialize(ref nb);
			stream.Serialize(ref nbMax);
		}
		else
		{
			stream.Serialize(ref nb);
			LevelLoader.nbPlayerCount = nb;
			stream.Serialize(ref nbMax);
			LevelLoader.nbPlayerMax = nbMax;
		}
	}
	private void OnDisconnectedFromServer(NetworkDisconnection msg)
	{
		LevelLoader.nbPlayerCount = 0;
		MenuManager.ShowMenu(ServerMenu);
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
			_register = true;
	}

	//Debug
	private void OnConnectedToServer()  //Client 
	{
		Debug.Log("Connected to server");
	}

	private void OnServerInitialized()
	{
		Debug.Log("Server initialized and ready");
	}

    private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    {
		Debug.Log("On failed to connect to Master Server: " + error);
		MenuManager.ShowMenu(ServerMenu);
    }

    private void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
		MenuManager.ShowMenu(ServerMenu);
    }
}
