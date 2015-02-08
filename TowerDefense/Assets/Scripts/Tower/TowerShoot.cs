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
        _enemiesTransform.Add(col.transform);
        if (_enemiesTransform.Count == 1)
            StartCoroutine("TryToShoot");
    }

    void OnTriggerExit(Collider col)
    {
        _enemiesTransform.Remove(col.transform);
        if (_enemiesTransform.Count == 0)
            StopCoroutine("TryToShoot");
    }

    IEnumerator TryToShoot()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            var ps = _projectilePoolManager.GetSpell();

            ps.gameObject.SetActive(true);

            ps.Transform.position = transform.position;

            ps.Rigidbody.velocity =
                (_enemiesTransform[0].position - transform.position).normalized * _projectileSpeed;

            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
