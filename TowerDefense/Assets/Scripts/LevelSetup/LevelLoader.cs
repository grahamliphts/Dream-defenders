using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    private int _lastLevelPrefix;
    private NetworkView _networkView;

    public void Start()
    {
        DontDestroyOnLoad(this);
        _lastLevelPrefix = 0;
        _networkView = GetComponent<NetworkView>();
        _networkView.group = 1;
    }

	public void LoadGame(Text players)
    {
		string nbPlayer = players.text;
		string[] subStrings = nbPlayer.Split('/');

		//Verification que tout les joueurs sont la 
		if(subStrings[0] == subStrings[1])
		{
			Network.RemoveRPCsInGroup(0);
			Network.RemoveRPCsInGroup(1);
			_networkView.RPC("LoadLevel", RPCMode.AllBuffered, "MainScene", _lastLevelPrefix + 1);
		}
	}

    [RPC]
    IEnumerator LoadLevel(string level, int levelPrefix)
    {
        _lastLevelPrefix = levelPrefix;

        Network.SetSendingEnabled(0, false);
        Network.isMessageQueueRunning = false;
        Network.SetLevelPrefix(levelPrefix);
        Application.LoadLevel(level);
        yield return null;

        Network.isMessageQueueRunning = true;
        Network.SetSendingEnabled(0, true);
		LevelStart.instance.OnLoadedLevel(true);
    }
}
