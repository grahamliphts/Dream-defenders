using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
    //Gui
    public GameObject serverList;
    public Text gui_input;

    public Text buttonText;
    public Button button;

    //Network
    private int _nbPlayers = 2;
	private int _listenPort = 4242; //le port d'écoute du serveur
    private const string _typeName = "TowerDefenseGameRavonyx";
    private string _gameName;
    private HostData[] _hostList;

	public GameObject net_player;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	public void StartServer () 
	{
        MasterServer.ipAddress = "87.106.110.31";
        MasterServer.port = 23466;
        Network.natFacilitatorIP = "87.106.110.31";
        Network.natFacilitatorPort = 24466;

        Network.InitializeServer(_nbPlayers, _listenPort, !Network.HavePublicAddress());
        _gameName = gui_input.text.ToString();
        MasterServer.RegisterHost(_typeName, gui_input.text);
        
		Debug.Log("Server initialized with: " + _nbPlayers + "\nNom: " + gui_input.text);
	}
    public void RefreshHostList()
    {
        MasterServer.RequestHostList(_typeName);
        if (_hostList != null)
        {
            for (int i = 0; i < _hostList.Length; i++)
            {
                Button b_object;
                b_object = Instantiate(button, new Vector3(0, i * 20, 0), Quaternion.identity) as Button;
                b_object.transform.parent = serverList.transform;

                buttonText.text = _gameName;
				Debug.Log(b_object.transform.localPosition);
            }
        }
        MasterServer.ClearHostList();
    }

    public void TestConnect()
    {
        MasterServer.RequestHostList(_typeName);
        if (_hostList != null)
        {
            foreach (HostData host in _hostList)
                JoinServer(host);
        }
    }

    public void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
        Application.LoadLevel("MainScene");
    }

	private void OnLevelWasLoaded()
	{
		if (Network.peerType == NetworkPeerType.Client) 
		{
				Debug.Log ("Instantiate");
				Network.Instantiate (net_player, new Vector3 (0, 0, 0), Quaternion.identity, 0);
		}
	}
    private void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            _hostList = MasterServer.PollHostList();
        Debug.Log(msEvent.ToString());
    }

    private void OnConnectedToServer()  //Client 
    {
        Debug.Log("Connected to server");
        Application.LoadLevel("MainScene");
    }

    /*private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to master server: " + error);
    }

    private void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
    }

	
    private void OnPlayerConnected(NetworkPlayer player) //Server
	{
		Debug.Log("Connecté !");

	}*/
}
