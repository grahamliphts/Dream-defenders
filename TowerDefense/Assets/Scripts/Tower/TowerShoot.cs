using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerShoot : MonoBehaviour 
{
    public SpellPoolManager _projectilePoolManager;
    public Transform SpawnPoint;
    public float _projectileSpeed;
    public float _shootDelay;
    private List<Transform> _enemiesTransform;

    void Start()
    {
        _enemiesTransform = new List<Transform>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ennemy")
        {
            _enemiesTransform.Add(col.transform);
            if (_enemiesTransform.Count == 1)
                StartCoroutine("TryToShoot");
        }
    }

    void OnTriggerExit(Collider col)
    {
        //Debug.Log(col.gameObject);
        if (col.gameObject.tag == "ennemy")
        {
            _enemiesTransform.Remove(col.transform);
            Debug.Log(_enemiesTransform.Count);
            if (_enemiesTransform.Count == 0)
                StopCoroutine("TryToShoot");
        }
    }

    IEnumerator TryToShoot()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            var ps = _projectilePoolManager.GetSpell();

            ps.gameObject.SetActive(true);
            ps.Transform.position = SpawnPoint.position;
            Physics.IgnoreCollision(ps.collider, transform.collider);
            ps.Rigidbody.AddForce((_enemiesTransform[0].position - transform.position).normalized * _projectileSpeed);
            //ps.Rigidbody.velocity = (
            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
