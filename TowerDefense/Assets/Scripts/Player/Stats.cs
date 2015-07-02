using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
	[SerializeField]
	private float _intelligence;
	[SerializeField]
	private float _robustesse;
	[SerializeField]
	private float _endurance;
	[SerializeField]
	private float _esprit;
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
	void Start()
	{
		_life = lifeMax;
		_mana = manaMax;
		_regen = 3;
		StartCoroutine("RegenMana");
	}

	IEnumerator RegenMana()
	{
		while (true)
		{
			//Debug.Log(_mana + " < " + manaMax);
			if (_mana < manaMax)
			{
				_mana = _mana + _regen;
				Debug.Log("Add mana " + _regen);
			}
				
			yield return new WaitForSeconds(_regenDelay);
		}
	}
	void OnCollisionEnter(Collision col)
	{
		//Debug.Log(col.gameObject.name);
		int count = 0;
		if (_life > 0)
		{
			for (int i = 0; i < _damages.Length; i++)
			{
				if (col.gameObject.tag == _damages[i].tag)
					_life = _life - _damages[i].damage;
				count++;
			}
		}
	}

	private void CalculateStat(int _power)
	{
	
		esprit = _power * 1.4f;
		_regen = esprit * 0.4f;

		endurance = _power * 1.5f;
		lifeMax += endurance * 1.5f;
		_life = lifeMax;

		robustesse = _power * 1.3f;
	}
}
