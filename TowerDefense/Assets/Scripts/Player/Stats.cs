using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
	private float _regen;
	public float regen
	{
		set
		{
			_regen = value;
		}
		get
		{
			return _regen;
		}
	}

	[SerializeField]
	private float _intelligence;
	public float intelligence
	{
		get
		{
			return _intelligence;
		}
		set
		{
			_intelligence = value;
		}
	}

	[SerializeField]
	private float _esprit;
	public float esprit
	{
		set
		{
			_esprit = value;
		}
		get
		{
			return _esprit;
		}
	}

	[SerializeField]
	private float _robustesse;
	public float robustesse
	{
		set
		{
			_robustesse = value;
		}
		get
		{
			return _robustesse;
		}
	}

	[SerializeField]
	private float _endurance;
	public float endurance
	{
		set
		{
			_endurance = value;
		}
		get
		{
			return _endurance;
		}
	}

	private float _damageReduction;
	public float damageReduction
	{
		get
		{
			return _damageReduction;
		}
		set
		{
			_damageReduction = value;
		}
	}

	private int _degatsAdd;
	public int degatsAdd
	{
		get
		{
			return _degatsAdd;
		}
		set
		{
			_degatsAdd = value;
		}
	}

	private float _mana;
	public float mana
	{
		set
		{
			_mana = value;
		}
		get
		{
			return _mana;
		}
	}
	private float _life;
	public float life
	{
		set
		{
			_life = value;
		}
		get
		{
			return _life;
		}
	}

	public float manaMax;
	public float lifeMax;

	[System.Serializable]
	public struct Damages
	{
		public string tag;
		public int damage;
	}

	[SerializeField]
	private int _degatsRecus;
	[SerializeField]
	private int _degatsRecusBase;

	[SerializeField]
	private int _regenDelay;
	private float _power;
	public LevelManager levelManager;
	
	

	private float[] _growFactors;
	public EnnemyManager enemyManager;

	void Start()
	{
		_life = lifeMax;
		_mana = manaMax;
		_regen = 3;
		_damageReduction = 0;
		StartCoroutine("RegenMana");
		_power = levelManager.power;
		_growFactors = levelManager.growFactors;
	}

	void Update()
	{
		if(_power != levelManager.power)
		{
			_power = levelManager.power;
			CalculateStat(_power);
			IncreaseDamageEnemies(_power * 1.6f);
			enemyManager.IncreaseDamageOnEnemies(_degatsAdd);
		}
	}

	IEnumerator RegenMana()
	{
		while (true)
		{
			if (_mana < manaMax)
				_mana = _mana + _regen;
			yield return new WaitForSeconds(_regenDelay);
		}
	}

	void IncreaseDamageEnemies(float value)
	{
		_degatsRecus = (int)(_degatsRecusBase + value);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "proj_enemy")
		{
			if (_life > 0)
				_life = _life - (_degatsRecus - _damageReduction);
		}
	}

	private void CalculateStat(double _power)
	{
		_robustesse = (float)_power * _growFactors[0];
		_damageReduction = _robustesse * 0.1f;

		_endurance = (float)_power * _growFactors[1];
		lifeMax += endurance * 1.5f;
		_life = lifeMax;

		_esprit = (float)_power * _growFactors[2];
		_regen = _esprit * 0.4f;

		_intelligence = (float)_power * _growFactors[3];
		_degatsAdd = (int)(_intelligence * 0.5f);
	}
}
