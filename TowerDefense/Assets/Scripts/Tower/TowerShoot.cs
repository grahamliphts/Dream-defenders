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

    void Update()
    {
        for(int i = 0; i < _enemiesTransform.Count; i++)
        {
            if (_enemiesTransform[i].gameObject.activeSelf == false)
                _enemiesTransform.Remove(_enemiesTransform[i]);
        }

        if(_enemiesTransform.Count == 0)
            StopCoroutine("TryToShoot");
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
            ps.newtransform.position = SpawnPoint.position;

            //Physics.IgnoreCollision(ps.GetComponent<Collider>(), transform.GetComponent<Collider>());
            ps.newrigidbody.AddForce((_enemiesTransform[0].position - transform.position).normalized * _projectileSpeed);

            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
