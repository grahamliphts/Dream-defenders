using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{
    public Transform SpawnPoint;
    public SpellPoolManager _spellPoolManager;
    public float _projectileSpeed;
    public float _shootDelay;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryToShoot();
            /*GameObject fireballShoot = Instantiate(Fireball, SpawnPoint.transform.position, Quaternion.identity) as GameObject;
            fireballShoot.rigidbody.AddForce(-transform.forward * 1500);*/

        }
    }

    void TryToShoot()
    {
        var spell = _spellPoolManager.GetSpell();
        spell.gameObject.SetActive(true);
        spell.Transform.position = transform.position;
        Debug.Log("Spell velocity " + spell.rigidbody.velocity);
        Debug.Log("Spell position " + spell.Transform.position);
        spell.Rigidbody.AddForce(transform.forward * 1500);
    }
}
