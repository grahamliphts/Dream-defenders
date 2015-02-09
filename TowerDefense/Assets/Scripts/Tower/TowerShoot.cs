using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerShoot : MonoBehaviour 
{
    public SpellPoolManager _projectilePoolManager;
    public GameObject _projectilePrefab;
    public float _projectileSpeed;
    public float _shootDelay;

    private List<Transform> _enemiesTransform;

    // Use this for initialization
    void Start()
    {
        _enemiesTransform = new List<Transform>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ennemy")
        {
            Debug.Log(col.gameObject.name);
            _enemiesTransform.Add(col.transform);
            if (_enemiesTransform.Count == 1)
                StartCoroutine("TryToShoot");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ennemy")
        {
            _enemiesTransform.Remove(col.transform);
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

            ps.Transform.position = transform.position;
            ps.rigidbody.rotation = ps.Transform.rotation;
            ps.Rigidbody.AddForce(transform.forward * 15000);
            /*ps.Rigidbody.velocity = (_enemiesTransform[0].position - transform.position).normalized * _projectileSpeed;
            Debug.Log((_enemiesTransform[0].position - transform.position).normalized);*/
            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
