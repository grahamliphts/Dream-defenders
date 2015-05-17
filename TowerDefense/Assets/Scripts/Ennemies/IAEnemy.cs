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
    private bool _bShoot;

	private NetworkView _networkView;
	private float _range;
	private Vector3 _posToShoot;

	void Start()
    {
        _bShoot = false;
        _recharging = false;
		//Debug.Log(_agent.destination);
		_networkView = GetComponent<NetworkView>();
    }

	void Update()
    {
		/*if (!LevelStart.instance.modeMulti || Network.isServer)
		{*/

		/*Debug.Log(_networkView);
		Debug.Log(_networkView.isMine);*/
		
		if (LevelStart.instance.modeMulti == false || _networkView.isMine)
		{
			if (_agent == null)
				return;
			_rangeNexus = Vector3.Distance(transform.position, ArrivalP.position);
			_range = Vector3.Distance(transform.position, _leader[0].position);
			//Debug.Log("Position Leader:" + _leader[0].position);
			Debug.Log("Destination:" + ArrivalP.position);
		}


		if (_range < backrange)
			transform.Translate(_backSpeed * Vector3.forward * Time.deltaTime);
		else if (_range < minRange)
		{
			Debug.Log("_range < minRange");
			_posToShoot = _leader[0].position;
			_bShoot = true;
			transform.LookAt(_leader[0]);
		}

		else if (_range <= chaseRange)
		{
			Debug.Log("_range <= chaseRange)");
			_agent.Stop();
			_bShoot = false;
			transform.LookAt(_leader[0]);
			transform.Translate(_speed * Vector3.forward * Time.deltaTime);
		}
		else
		{
			Debug.Log("else");
			_bShoot = false;
			_agent.Resume();
		}

		if(_rangeNexus < minRange)
		{
			Debug.Log("_rangeNexus");
			_agent.Stop();
			_posToShoot = ArrivalP.position;
			_bShoot = true;
		}
		Shooting();
			
		//}
	} 

    public void SetAgent(NavMeshAgent agent)
    {
        _agent = agent;
    }

	public void SetArrivalP(Transform arrivalP)
	{
		ArrivalP = arrivalP;
	}

    public void AddLeader(Transform leader)
    {
        _leader.Add(leader);
    }

	void Shooting()
	{
		if (_bShoot)
		{
			if (!_recharging)
			{
				TryToShoot();
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
	}

    void TryToShoot()
    {
        var ps = ProjectilePool.GetSpell();
        ps.gameObject.SetActive(true);
        ps.newtransform.position = SpawnPoint.position;
		//ps.newrigidbody.velocity = new Vector3(0, 0, 0);
		ps.newrigidbody.AddForce((_posToShoot - transform.position).normalized * _projectileSpeed);
    }

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		float rangeEnemy = 0;
		if (stream.isWriting)
		{
			rangeEnemy = _range;
			Debug.Log("range send:" + rangeEnemy);
			stream.Serialize(ref rangeEnemy);
		}

		else
		{
			stream.Serialize(ref rangeEnemy);
			_range = rangeEnemy;
			Debug.Log("range get:" + _range);
		}
	}
}

