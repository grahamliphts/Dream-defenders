using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RessourceGUI : MonoBehaviour 
{
	public Text[] valueTower;
	private int[] _towerAvailables;

	void Start ()
	{
		_towerAvailables = LevelStart.instance.towerAvailables;
	}
	
	void Update () 
	{
		for(int i = 0; i < valueTower.Length; i++)
		{
			int value = _towerAvailables[i];
			valueTower[i].text = value.ToString();
		}
	}
}
