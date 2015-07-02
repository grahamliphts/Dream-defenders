using UnityEngine;
using System.Collections.Generic;

public class MonsterLifeManager : MonoBehaviour 
{
    private float _life;
	[SerializeField]
	private float _lifeMax;
	private int _xpMax;
	private int _xpMin;

	private Color _healthBarColor;

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

	public LevelManager levelManager;

    public Material materialModel;
	private Material _matHeathBar;
    public GameObject healthBar;

	private NetworkView _networkView;
	private bool _died;

	void Awake()
	{
		healthBar.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialModel);
		_matHeathBar = healthBar.GetComponent<Renderer>().material;
		_healthBarColor = _matHeathBar.GetColor("_Color");
		_networkView = GetComponent<NetworkView>();
		_xpMax = 20;
		_xpMin = 10;

	}

    void OnCollisionEnter(Collision col)
    {
		if (_died)
			return;
		
		if(!LevelStart.instance.modeMulti || Network.isServer)
		{
			int count = 0;
			for (int i = 0; i < _damages.Length; i++)
			{
				if (col.gameObject.tag == _damages[i].tag)
					_life = _life - _damages[i].damage;
				count++;
			}
			if (LevelStart.instance.modeMulti)
			{
				_networkView.RPC("SyncLifeEnemy", RPCMode.All, _life);
				if (_life <= 0)
				{
					levelManager.xpGained += Random.Range(_xpMin, _xpMax);
					levelManager.money += 20;
					_died = true;	
				}
					
			}
			else
			{
				_matHeathBar.SetFloat("_HP", _life);
				SetColorLife(_life);

				if (_life <= 0)
				{
					DestroyEnemy();
					levelManager.xpGained += Random.Range(_xpMin, _xpMax);
					levelManager.money += 20;
					_died = true;
				}
			}
		}
    }

	[RPC]
	private void SyncLifeEnemy(float life)
	{
		_life = life;
		_matHeathBar.SetFloat("_HP", _life);
		SetColorLife(_life);

		if (_life <= 0)
			DestroyEnemy();
	}

	private void DestroyEnemy()
	{
		transform.position = new Vector3(1000, 1000, 1000);
		gameObject.SetActive(false);
	}

	private void SetColorLife(float life)
	{
		if(_life > _lifeMax/2)
			_matHeathBar.SetColor("_Color", _healthBarColor);
		else if(life <= _lifeMax/2 && life >= _lifeMax/4)
			_matHeathBar.SetColor("_Color", Color.yellow);
		else if (life < _lifeMax/4)
			_matHeathBar.SetColor("_Color", Color.red);
	}

	void OnEnable ()
	{
		_matHeathBar.SetFloat("_HP", _lifeMax);
		_died = false;
		_life = _lifeMax;
		SetColorLife(_life);
	}
}
