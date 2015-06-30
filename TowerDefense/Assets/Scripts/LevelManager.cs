using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour 
{
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

	private double _power;
	private int _level;
	public int level
	{
		get
		{
			return _level;
		}
	}

	void Start () 
	{
		_level = 1;
		CalculateXpNeed(_level);
		CalculatePower(_level);
		CalculatePower(2);
		CalculatePower(3);
		CalculatePower(4);
		CalculatePower(5);
	}

	void Update()
	{
		//Afficher GUI
		Debug.Log(_xpGained + "/" + _xpNeed + " Power : " + _power);
		if (_xpGained >= _xpNeed)
			Upgrade();
	}

	void CalculateXpNeed(int level)
	{
		_xpNeed = Math.Pow((level * 8), 2);
	}

	void CalculatePower(int level)
	{
		_power =  (int) Math.Round(100 - 100 * Math.Exp(-0.05 * level));
		Debug.Log(_power);
	}

	void Upgrade()
	{
		_level++;
		CalculateXpNeed(_level);
		CalculatePower(_level);
		_xpGained = 0;
	}
}
