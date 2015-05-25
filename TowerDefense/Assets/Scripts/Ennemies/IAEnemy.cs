using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IAEnemy : MonoBehaviour 
{
    [SerializeField]
    private List<Transform> _leader;  
	public Transform ArrivalP;
    public Transform SpawnPoint;
    public SpellPoolManager ProjectilePool;

    [SerializeField]
    private float _projectileSpeed;
    [SerializeField]
    private float _shootDelay;
    private float _shootTimer;
    private bool _recharging;
	private NavMeshAgent _agent;

	// This is the speed with which the follower will pursue
    [SerializeField]
	private float _speed = 4f;
    private float _backSpeed = -6f;
	private float _rangeNexus;

	// This is the range at which to pursue
	public float chaseRange;
    public float minRange;
    public float backrange;

	private NetworkView _networkView;
	private float _range;
	

	void Start()
    {
        _recharging = false;
		_agent = GetComponent<NavMeshAgent>();
		_networkView = GetComponent<NetworkView>();
    }

	void Update()
    {
		bool bShoot = false;
		Vector3 posToShoot = new Vector3(0, 0, 0);

		if(LevelStart.instance.modeMulti == false || Network.isServer)
		{
			if (_agent == null)
				return;
			_rangeNexus = Vector3.Distance(transform.position, ArrivalP.position);
			_range = Vector3.Distance(transform.position, _leader[0].position);

			if (_range < backrange)
				transform.Translate(_backSpeed * Vector3.forward * Time.deltaTime);
			else if (_range < minRange)
			{
				posToShoot = _leader[0].position;
				bShoot = true;
				transform.LookAt(_leader[0]);
			}

			else if (_range <= chaseRange)
			{
				_agent.Stop();
				bShoot = false;
				transform.LookAt(_leader[0]);
				transform.Translate(_speed * Vector3.forward * Time.deltaTime);
			}
			else
			{
				bShoot = false;
				_agent.Resume();
			}

			//Dans le range du nexus
			if (_rangeNexus < minRange)
			{
				_agent.Stop();
				posToShoot = ArrivalP.position;
				bShoot = true;
			}

			if (bShoot)
			{
				if(LevelStart.instance.modeMulti)
					_networkView.RPC("SyncShootEnemy", RPCMode.All, posToShoot);
				else
					Shooting(posToShoot);
			}
		}
	} 

	public void SetArrivalP(Transform arrivalP)
	{
		ArrivalP = arrivalP;
	}

    public void AddLeader(Transform leader)
    {
        _leader.Add(leader);
    }

	[RPC]
	private void SyncShootEnemy(Vector3 posToShoot)
	{
		Shooting(posToShoot);
	}

	void Shooting(Vector3 posToShoot)
	{
		if (!_recharging)
		{
			TryToShoot(posToShoot);
			_recharging = true;
			_shootTimer = _shootDelay;
		}
		else
		{
			_shootTimer -= Time.deltaTime;
			if (_shootTimer <= 0.0f)
				_recharging = false;
		}
	}



	void TryToShoot(Vector3 posToShoot)
    {
        var ps = ProjectilePool.GetSpell();
        ps.gameObject.SetActive(true);
        ps.newtransform.position = SpawnPoint.position;
		ps.newrigidbody.AddForce((posToShoot - transform.position).normalized * _projectileSpeed);
    }
}

