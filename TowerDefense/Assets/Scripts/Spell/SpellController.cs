using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpawnPoint;
    public SpellPoolManager _spellPoolManager;

	private NetworkView _networkView;
	private Transform _firePoint;

	void Start()
	{
		_networkView = GetComponent<NetworkView>();
		_firePoint = transform.GetChild(2);
		_spellPoolManager = LevelStart.instance.spellPool;
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
		}
    }


	[RPC]
	void SyncShoot()
	{
		TryToShoot();
	}

    void TryToShoot()
    {
		var spell = _spellPoolManager.GetSpell();
        spell.gameObject.SetActive(true);
        spell.newtransform.position = _firePoint.position;

		//-transform.forward cause player is inverted
        spell.newrigidbody.AddForce(_firePoint.forward * 1500);
		/* Script Mouse Spell
		Vector3 mousePos;
		mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		spell.newtransform.LookAt(mousePos);
		spell.newrigidbody.AddForce(spell.newtransform.forward * 1500);*/
    }
}
