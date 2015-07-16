using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RessourceGUI : MonoBehaviour 
{
	public LevelManager levelManager;

	public Text[] valueTower;
	private int[] _towerAvailables;
	public Text xp;
	public Text level;
	public Text money;

	void Start ()
	{
		_towerAvailables = LevelStart.instance.towerAvailables;
	}
	
	void Update () 
	{
		xp.text = "XP : " + levelManager.xpGained + "/" + levelManager.xpNeed;
		level.text = "Niveau : " + levelManager.level.ToString();
		money.text = "Argent : " + levelManager.money.ToString();
		for(int i = 0; i < valueTower.Length; i++)
		{
			int value = _towerAvailables[i];
			valueTower[i].text = value.ToString();
		}
	}
}
