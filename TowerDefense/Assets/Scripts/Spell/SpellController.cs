using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpawnPoint;
    public SpellPoolManager _spellPoolManager;

	private bool _modeConstruction;
    void Update()
    {
		if(_modeConstruction == false)
		{
			if (Input.GetMouseButtonDown(0))
				TryToShoot();
		}
    }

    void TryToShoot()
    {
        var spell = _spellPoolManager.GetSpell();
        spell.gameObject.SetActive(true);
        spell.newtransform.position = transform.position;
        spell.newrigidbody.AddForce(transform.forward * 1500);
		/* Script Mouse Spell
		Vector3 mousePos;
		mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		spell.newtransform.LookAt(mousePos);
		spell.newrigidbody.AddForce(spell.newtransform.forward * 1500);*/
    }

	public void SetModeConstruction(bool mode)
	{
		_modeConstruction = mode;
	}

	public void SetSpellPoolManager(SpellPoolManager spellPool)
	{
		_spellPoolManager = spellPool;
	}
}
