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
	private Damages[] _damages;
	public Damages[] damages
	{
		set
		{
			_damages = value;
		}
		get
		{
			return _damages;
		}
	}

	[SerializeField]
	private int _regenDelay;
	private float _power;
	public LevelManager levelManager;
	private float _damageReduction;

	void Start()
	{
		_life = lifeMax;
		_mana = manaMax;
		_regen = 3;
		StartCoroutine("RegenMana");
	}

	void Update()
	{
		if(_power != levelManager.power)
		{
			_power = levelManager.power;
			CalculateStat(_power);
			IncreaseDamageEnemies(_power);
		}
	}

	IEnumerator RegenMana()
	{
		while (true)
		{
			if (_mana < manaMax)
			{
				_mana = _mana + _regen;
				Debug.Log("Add mana " + _regen);
			}
				
			yield return new WaitForSeconds(_regenDelay);
		}
	}

	void IncreaseDamageEnemies(float factor)
	{
		for (int i = 0; i < _damages.Length; i++)
		{
			if (_damages[i].tag == "proj_enemy")
			{
				_damages[i].damage = (int)factor;
			}
		}
	}

	void OnCollisionEnter(Collision col)
	{
		int count = 0;
		if (_life > 0)
		{
			for (int i = 0; i < _damages.Length; i++)
			{
				if (col.gameObject.tag == _damages[i].tag)
					_life = _life - (_damages[i].damage - _damageReduction);
				count++;
			}
		}
	}

	private void CalculateStat(double _power)
	{

		_esprit = (float)_power * 1.4f;
		_regen = _esprit * 0.4f;

		_endurance = (float)_power * 1.5f;
		lifeMax += endurance * 1.5f;
		_life = lifeMax;

		_robustesse = (float)_power * 1.3f;
		_damageReduction = _robustesse * 0.1f;

		Debug.Log(_esprit);
		Debug.Log(_regen);
		Debug.Log(_endurance);
		Debug.Log(_robustesse);
	}
}
