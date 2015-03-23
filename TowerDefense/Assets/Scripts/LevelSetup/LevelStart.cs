using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour 
{
    public GameObject netPlayer;
    void OnNetworkLoadedLevel()
    {
        Debug.Log("Level was loaded");
        GameObject player = Network.Instantiate(netPlayer, new Vector3(5, 1, 5), Quaternion.identity, 0) as GameObject;
		Transform targetCamera = null;
		for (int i = 0; i < player.transform.childCount; i++)
		{
			Transform child = player.transform.GetChild(i);
			if (child.name == "TargetCamera")
			{
				targetCamera = child;
				break;
			}
		}
		if(targetCamera != null)
			Camera.main.gameObject.GetComponent<CameraController>().target = targetCamera;
    }
}
