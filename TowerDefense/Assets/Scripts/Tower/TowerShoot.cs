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
	private NetworkView _networkView;

    void Start()
    {
		_networkView = GetComponent<NetworkView>();
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
			if (LevelStart.instance.modeMulti == false || Network.isServer)
			{
				if (_enemiesTransform.Count == 1)
				{
					if(LevelStart.instance.modeMulti)
						_networkView.RPC("StartShootTower", RPCMode.All);
					else
						StartCoroutine("TryToShoot");
				}
			}
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ennemy")
        {
            _enemiesTransform.Remove(col.transform);
			if(LevelStart.instance.modeMulti && Network.isServer)
				_networkView.RPC("StopShootTower", RPCMode.All);
			else
				StopCoroutine("TryToShoot");
        }
    }

	[RPC]
	private void StartShootTower()
	{
		StartCoroutine("TryToShoot");
	}

	[RPC]
	private void StopShootTower()
	{
		StopCoroutine("TryToShoot");
	}

    IEnumerator TryToShoot()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            var ps = _projectilePoolManager.GetSpell();
            ps.gameObject.SetActive(true);
            ps.newtransform.position = SpawnPoint.position;
			//Fix
			if(_enemiesTransform[0] != null)
				ps.newrigidbody.AddForce((_enemiesTransform[0].position - SpawnPoint.position).normalized * _projectileSpeed);

            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
