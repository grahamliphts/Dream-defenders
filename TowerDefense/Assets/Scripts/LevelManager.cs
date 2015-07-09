using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour 
{
	[SerializeField]
	private float[] _growFactors;
	public float[] growFactors
	{
		get
		{
			return _growFactors;
		}
		set
		{
			_growFactors = value;
		}
	}

	private int _money;
	public int money
	{
		set
		{
			_money = value;
		}
		get
		{
			return _money;
		}
	}

	private double _xpGained;
	public double xpGained
	{
		set
		{
			_xpGained = value;
		}
		get
		{
			return _xpGained;
		}
	}
	private double _xpNeed;
	public double xpNeed
	{
		set
		{
			_xpNeed = value;
		}
		get
		{
			return _xpNeed;
		}
	}

	private float _power;
	public float power
	{
		get
		{
			return _power;
		}
		set
		{
			_power = value;
		}
	}

	private int _level;
	public int level
	{
		get
		{
			return _level;
		}
	}

	private int _moneyEnemy;
	private int _xpEnemy;

	void Start () 
	{
		_money = 0;
		_level = 1;
		CalculateXpNeed(_level);
		CalculatePower(_level);
	}

	void Update()
	{
		//NextLevel
		if (_xpGained >= _xpNeed)
			Upgrade();		
	}

	void CalculateXpNeed(int level)
	{
		_xpNeed = Math.Pow((level * 8), 2);
	}

	void CalculatePower(int level)
	{
		_power =  (float) Math.Round(100 - 100 * Math.Exp(-0.05 * level),2);
	}

	void Upgrade()
	{
		_level++;
		CalculateXpNeed(_level);
		CalculatePower(_level);
		_xpGained = 0;
	}
}
