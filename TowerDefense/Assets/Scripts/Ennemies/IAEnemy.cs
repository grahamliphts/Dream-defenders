using UnityEngine;
using System.Collections;

public class IAEnemy : MonoBehaviour 
{
	// This is the object to follow
	public Transform leader;  

	//this is the arrival point
	public Transform ArrivalP;  

	private NavMeshAgent _agent;

	// This is the speed with which the follower will pursue
	public float speed = 4f;
	
	// This is the range at which to pursue
	public float chaseRange = 8f;

    public float minRange = 3f;

    public float backrange = 2f;
	// This is used to store the distance between the two objects.
	private float range;

	// fireball
	//public GameObject Fireball;
	//public Transform SpawnPoint;

	void Start()
    {
        _agent.SetDestination(ArrivalP.position);
        NavMeshPath path = new NavMeshPath();
        _agent.CalculatePath(ArrivalP.position, path);
        if (path.status == NavMeshPathStatus.PathPartial)
        {
           // Debug.Log("fail");
        }
        else
        {
         //   Debug.Log("reachable");
        }
    }
	void Update()
    {
       // Debug.Log(_agent.pathStatus);
		// Calculate the distance between the follower and the leader.
        if(_agent == null)
            return;
        if (_agent.pathStatus == NavMeshPathStatus.PathComplete)
            _agent.SetDestination(ArrivalP.position + new Vector3(1, 0, 0));

		range = Vector3.Distance( transform.position,leader.position );
        if(range < backrange)
        {
           // Debug.Log("Stop");
            _agent.Stop();
            transform.LookAt(leader);
            transform.Translate(-((speed+2) * Vector3.forward * Time.deltaTime));
        }
        else if(range < minRange )
        {
           // Debug.Log("Stop");
            _agent.Stop();
            transform.LookAt(leader);
        }
		else if ( range <= chaseRange )
        {
          //  Debug.Log("Stop");
            _agent.Stop();
			transform.LookAt(leader);
			transform.Translate( speed * Vector3.forward * Time.deltaTime);

		}
        /*else
          {
              Debug.Log("Resume");
              _agent.Resume();
              return;
          } */

    } 

    public void SetAgent(NavMeshAgent agent)
    {
        _agent = agent;
    }
}

