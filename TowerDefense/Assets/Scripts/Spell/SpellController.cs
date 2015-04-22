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

	void Start()
	{
		_networkView = GetComponent<NetworkView>();
		_firePoint = transform.GetChild(2);
		_spellPoolManager = LevelStart.instance.spellPool;
		_currentSpellPool = LevelStart.instance.currentSpellPool;
		_type = 0; //FirePool
	}
    void Update()
    {
		if (LoopManager.modeConstruction == false)
		{
			if (LevelStart.instance.modeMulti == false || _networkView.isMine)
			{
				if (Input.GetMouseButtonDown(0))// && (LevelStart.instance.modeMulti == false || _networkView.isMine))
				{
					if (LevelStart.instance.modeMulti == false)
						TryToShoot(_type);
					else
						_networkView.RPC("SyncShoot", RPCMode.All, _type);
				}

				if (Input.GetKey("1"))
					_type = 0;
				else if (Input.GetKey("2"))
					_type = 1;
				else if (Input.GetKey("3"))
					_type = 2;
				else if (Input.GetKey("4"))
					_type = 3;

				Debug.Log(_currentSpellPool);
			}
			
		}
    }


	[RPC]
	void SyncShoot(int type)
	{
		TryToShoot(type);
	}

	void TryToShoot(int type)
    {
		_currentSpellPool = _spellPoolManager[type];
		var spell = _currentSpellPool.GetSpell();
        spell.gameObject.SetActive(true);
        spell.newtransform.position = _firePoint.position;
        spell.newrigidbody.AddForce(_firePoint.forward * 1500);

		/* Script Mouse Spell
		Vector3 mousePos;
		mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		spell.newtransform.LookAt(mousePos);
		spell.newrigidbody.AddForce(spell.newtransform.forward * 1500);*/
    }
}
