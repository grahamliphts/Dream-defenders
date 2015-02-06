using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpawnPoint;
    public SpellPoolManager _spellPoolManager;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryToShoot();
    }

    void TryToShoot()
    {
        var spell = _spellPoolManager.GetSpell();
        spell.gameObject.SetActive(true);
        spell.Transform.position = transform.position;
        spell.Rigidbody.AddForce(transform.forward * 1500);
    }
}
