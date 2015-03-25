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

	// This is the range at which to pursue
	public float chaseRange;
    public float minRange;
    public float backrange;
    private bool _bShoot;

	void Start()
    {
        _bShoot = false;
        _recharging = false;
    }

	void Update()
    {
        if(_agent == null)
            return;
        // Calculate the distance between the follower and the leader.
		//Debug.Log(gameObject.name + " " + _leader.Count);
        float range = Vector3.Distance(transform.position, _leader[0].position);
        if (range < backrange)
        {
            _agent.Stop();
            transform.LookAt(_leader[0]);
            _bShoot = true;
            transform.Translate(_backSpeed * Vector3.forward * Time.deltaTime);
        }
        else if (range < minRange)
        {
            _agent.Stop();
            _bShoot = true;
            transform.LookAt(_leader[0]);
        }
        else if (range <= chaseRange)
        {
            _agent.Stop();
            _bShoot = false;
            transform.LookAt(_leader[0]);
            transform.Translate(_speed * Vector3.forward * Time.deltaTime);
        }
        else
        {
            _bShoot = false;
            _agent.Resume();
        }

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
        else
        {
            _bShoot = false;
         
        }
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

    void TryToShoot()
    {
        var ps = ProjectilePool.GetSpell();
        ps.gameObject.SetActive(true);
		//Debug.Log(ps.newrigidbody.velocity);
        ps.newtransform.position = SpawnPoint.position;
		ps.newrigidbody.velocity = new Vector3(0, 0, 0);
        ps.newrigidbody.AddForce((_leader[0].position - transform.position).normalized * _projectileSpeed);
    }
}

