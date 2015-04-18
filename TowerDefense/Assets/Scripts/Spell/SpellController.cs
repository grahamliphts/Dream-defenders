using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpawnPoint;
    private SpellPoolManager []_spellPoolManager;
	private SpellPoolManager _currentSpellPool;
	private NetworkView _networkView;
	private Transform _firePoint;

	void Start()
	{
		_networkView = GetComponent<NetworkView>();
		_firePoint = transform.GetChild(2);
		_spellPoolManager = LevelStart.instance.spellPool;
		_currentSpellPool = LevelStart.instance.currentSpellPool;
	}
    void Update()
    {
		if (LoopManager.modeConstruction == false)
		{
			if (Input.GetMouseButtonDown(0) && (LevelStart.instance.modeMulti == false || _networkView.isMine))
			{
				if (LevelStart.instance.modeMulti == false)
					TryToShoot();
				else
					_networkView.RPC("SyncShoot", RPCMode.All);
			}

			if (Input.GetKey("1"))
				_currentSpellPool = _spellPoolManager[0];
			else if (Input.GetKey("2"))
				_currentSpellPool = _spellPoolManager[1];
			else if (Input.GetKey("3"))
				_currentSpellPool = _spellPoolManager[2];
			else if (Input.GetKey("4"))
				_currentSpellPool = _spellPoolManager[3];
		}
    }


	[RPC]
	void SyncShoot()
	{
		TryToShoot();
	}

    void TryToShoot()
    {
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
