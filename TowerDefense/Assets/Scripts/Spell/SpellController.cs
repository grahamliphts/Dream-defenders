using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpawnPoint;
    private SpellPoolManager []_spellPoolManager;
	private SpellPoolManager _currentSpellPool;
	private NetworkView _networkView;
	private Transform _firePoint;
	private int _type;

	private bool _isMine;
	private Stats _stats;
	[SerializeField]
	private int _costSpell;
	private bool _reset;

	void Start()
	{
		_networkView = GetComponent<NetworkView>();
		_isMine = GetComponent<CharacController>().isMine;
		_firePoint = transform.GetChild(2);
		_spellPoolManager = LevelStart.instance.spellPool;
		_currentSpellPool = LevelStart.instance.currentSpellPool;
		_type = 0; //FirePool
		_stats = GetComponent<Stats>();
	}
    void Update()
    {
		if (!LoopManager.modeConstruction)
		{
			if (!LevelStart.instance.modeMulti || _isMine)
			{
				if (Input.GetMouseButtonDown(0) && _stats.mana >= _costSpell)
				{
					if (!LevelStart.instance.modeMulti)
						StartCoroutine("TryToShoot",_type);
					else
						_networkView.RPC("SyncShoot", RPCMode.All, _type);
					_stats.mana -= _costSpell;
				}

				if (Input.GetKey("1"))
				{
					_type = 0;
					_reset = false;
				}
					
				else if (Input.GetKey("2"))
				{
					_type = 1;
					_reset = false;
				}
				else if (Input.GetKey("3"))
				{
					_type = 2;
					_reset = false;
				}
				else if (Input.GetKey("4"))
				{
					_type = 3;
					_reset = false;
				}
			}
		}
		else if (LoopManager.modeConstruction == true && _reset == false)
		{
			_type = 0;
			_reset = true;
		}
    }

	[RPC]
	void SyncShoot(int type)
	{
		StartCoroutine("TryToShoot",type);
	}

	public IEnumerator TryToShoot(int type)
	{
		yield return new WaitForFixedUpdate();
		_currentSpellPool = _spellPoolManager[type];
		var spell = _currentSpellPool.GetSpell();
		spell.mtransform.position = _firePoint.position;
		spell.gameObject.SetActive(true);
		spell.mrigidbody.isKinematic = false;
		spell.mrigidbody.AddForce(_firePoint.forward * 1500);
	}
}
