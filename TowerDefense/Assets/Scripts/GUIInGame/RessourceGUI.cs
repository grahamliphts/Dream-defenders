using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RessourceGUI : MonoBehaviour 
{
	public Text[] valueTower;
	private TowerPoolManager[] _towerPool;

	void Start ()
	{
		_towerPool = LevelStart.instance.towerPool;
	}
	
	void Update () 
	{
		for(int i = 0; i < valueTower.Length; i++)
		{
			int value = _towerPool[i].GetTowerNumberMax() - _towerPool[i].GetTowersNumber();
			valueTower[i].text = value.ToString();
		}
	}
}
