using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaBarManager : MonoBehaviour 
{
	public Scrollbar ManaPlayerValue;
	private Stats _stats;
	public Stats stats
	{
		set
		{
			_stats = value;
		}
		get
		{
			return _stats;
		}
	}
	private GameObject _player;
	public GameObject player
	{
		set
		{
			_player = value;
		}
		get
		{
			return _player;
		}
	}

	private float _manaBarValue;

	void Update()
	{
		if (_stats == null)
			return;
		if (_stats.mana <= _stats.manaMax)
		{
			_manaBarValue = _stats.mana / _stats.manaMax;
			ManaPlayerValue.size = _manaBarValue;
		}
	}
}
