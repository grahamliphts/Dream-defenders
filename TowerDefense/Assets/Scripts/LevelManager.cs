﻿using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour 
{
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
		_money = 0;
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
	}

	void Upgrade()
	{
		_level++;
		CalculateXpNeed(_level);
		CalculatePower(_level);
		_xpGained = 0;
	}
}
