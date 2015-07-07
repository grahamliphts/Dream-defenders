using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour 
{

    private int _lastLevelPrefix;
    private NetworkView _networkView;

	[SerializeField]
	private int _nbPlayerCount;
	public int nbPlayerCount
	{
		get
		{
			return _nbPlayerCount;
		}
		set
		{
			_nbPlayerCount = value;
		}
	}
	[SerializeField]
	private int _nbPlayerMax;
	public int nbPlayerMax
	{
		get
		{
			return _nbPlayerMax;
		}
		set
		{
			_nbPlayerMax = value;
		}
	}

	public static LevelLoader _instance;

    public void Start()
    {
		if (!_instance)
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
        _lastLevelPrefix = 0;
        _networkView = GetComponent<NetworkView>();
        _networkView.group = 1;
    }

	public void LoadGame()
    {
		//Verification que tout les joueurs sont la 
		if(_nbPlayerCount == _nbPlayerMax)
		{
			Network.RemoveRPCsInGroup(0);
			Network.RemoveRPCsInGroup(1);
			_networkView.RPC("LoadLevel", RPCMode.AllBuffered, "MainScene", _lastLevelPrefix + 1);
		}
	}

    [RPC]
    IEnumerator LoadLevel(string level, int levelPrefix)
    {
		NetworkPlayer networkPlayer;
		networkPlayer = Network.player;

        _lastLevelPrefix = levelPrefix;
		
        Network.SetSendingEnabled(0, false);
        Network.isMessageQueueRunning = false;
        Network.SetLevelPrefix(levelPrefix);
        Application.LoadLevel(level);
        yield return null;


		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);
		LevelStart.instance.OnLoadedLevel("Network", int.Parse(networkPlayer.ToString()));
    }
}
