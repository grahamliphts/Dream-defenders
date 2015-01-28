using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
    //Gui
    public GameObject serverList;
    public Text serverName;

    public Button createButton;
    public Button modelButton;

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
        Network.InitializeServer(_nbPlayers, _listenPort, !Network.HavePublicAddress());
         
        if (serverName.text != "")
            MasterServer.RegisterHost(_typeName, serverName.text);
	}

    public void RefreshHostList()
    {
        int i = 0;
        for (i = 0; i < serverList.transform.childCount; i++)
           Destroy(serverList.transform.GetChild(i).gameObject);
        MasterServer.RequestHostList(_typeName);
    }

    public void Update()
    {
        if (_hostList != null)
        {
            float anchorMinX = 0.1f;
            float anchorMaxX = 0.8f;
            Debug.Log("Nb Host:" + _hostList.Length);
            for (int i = 0; i < _hostList.Length; i++)
            {
                Button b_object;
                b_object = Instantiate(modelButton) as Button;
                b_object.transform.parent = serverList.transform;

                RectTransform rectTransform = b_object.GetComponent<RectTransform>();
                rectTransform.anchorMax = new Vector2(anchorMaxX,  1.0f - 0.1f*(i + 1));
                rectTransform.anchorMin = new Vector2(anchorMinX, 1.0f - 0.1f*(i + 2));
                rectTransform.sizeDelta = new Vector2(130, 50);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);

                

                Vector3 positionButton = b_object.transform.position;
                Vector3 positionParent = serverList.transform.position;
               // b_object.transform.position = new Vector3(0 + positionParent.x, positionButton.y + positionParent.y, 0);

                b_object.GetComponentInChildren<Text>().text = _hostList[i].gameName;
            }
            _hostList = null;
        }
        //MasterServer.ClearHostList();
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
        {
            _hostList = MasterServer.PollHostList();
            
        }
        Debug.Log(msEvent.ToString());
    }

    private void OnConnectedToServer()  //Client 
    {
        Debug.Log("Connected to server");
        Application.LoadLevel("MainScene");
    }

    private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
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

	}
}
