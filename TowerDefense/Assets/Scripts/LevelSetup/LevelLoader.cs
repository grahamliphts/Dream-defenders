using UnityEngine;
using System.Collections;

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
    public void LoadGame()
    {
        Network.RemoveRPCsInGroup(0);
        Network.RemoveRPCsInGroup(1);
        _networkView.RPC("LoadLevel", RPCMode.AllBuffered, "MainScene", _lastLevelPrefix + 1);
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

        var gameObjects = FindObjectsOfType<GameObject>();
		foreach (var go in gameObjects)
			go.SendMessage("OnLoadedLevel", true, SendMessageOptions.DontRequireReceiver);
    }
}
