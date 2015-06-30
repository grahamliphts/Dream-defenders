using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NamePlayer : MonoBehaviour 
{
	void Start () 
	{
		Text text = GetComponent<Text>();
		NetworkPlayer player = Network.player;
		if (int.Parse(player.ToString()) < 0)
			text.text = "";
		else
			text.text = "Player " + player.ToString();
	}
}
