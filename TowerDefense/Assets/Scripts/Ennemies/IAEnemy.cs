using UnityEngine;
using System.Collections;

public class IAEnemy : MonoBehaviour 
{
	public Transform leader;  
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
	public float speed = 4f;
	// This is the range at which to pursue
	public float chaseRange;
    public float minRange;
    public float backrange;
	// This is used to store the distance between the two objects.
	private float range;
    private bool bShoot;

	void Start()
    {
        _agent.SetDestination(ArrivalP.position);
        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(ArrivalP.position, path);
        bShoot = false;
        _recharging = false;
    }
	void Update()
    {
		// Calculate the distance between the follower and the leader.
        if(_agent == null)
            return;
        if (_agent.pathStatus == NavMeshPathStatus.PathComplete)
            _agent.SetDestination(ArrivalP.position + new Vector3(1, 0, 0));

		range = Vector3.Distance( transform.position,leader.position );
        if (range < chaseRange && range >= minRange)
        {
            //_agent.Stop();
            transform.LookAt(leader);
            _agent.SetDestination(leader.position);
            //transform.Translate(-(speed * Vector3.forward * Time.deltaTime));
        }
        else if (range < minRange)
        {
            _agent.Stop();
            transform.LookAt(leader);
            bShoot = true;
            
        }
       /* else
           _agent.Resume();
		else if ( range <= chaseRange )
        {
            _agent.Stop();
			transform.LookAt(leader);
			transform.Translate( speed * Vector3.forward * Time.deltaTime);
		}*/
        if (bShoot)
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
            bShoot = false;
         
        }
    } 

    public void SetAgent(NavMeshAgent agent)
    {
        _agent = agent;
    }

    void TryToShoot()
    {
        var ps = ProjectilePool.GetSpell();
        ps.gameObject.SetActive(true);
        ps.Transform.position = SpawnPoint.position;
        ps.Rigidbody.AddForce((leader.position - transform.position).normalized * _projectileSpeed);
    }
}

