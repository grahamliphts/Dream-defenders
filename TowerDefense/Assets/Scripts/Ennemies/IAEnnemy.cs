using UnityEngine;
using System.Collections;

public class IAEnnemy : MonoBehaviour 
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
		_agent = GetComponent<NavMeshAgent>();
		_agent.SetDestination(ArrivalP.position);
	}
	
	void Update(){
		
		// Calculate the distance between the follower and the leader.
		range = Vector3.Distance( transform.position,leader.position );
        if(range < backrange)
        {
            _agent.Stop();
            transform.LookAt(leader);
            transform.Translate(-((speed+2) * Vector3.forward * Time.deltaTime));
        }
        else if(range < minRange )
        {
            _agent.Stop();
            transform.LookAt(leader);
        }
		else if ( range <= chaseRange ){
			
			_agent.Stop();
			transform.LookAt(leader);
			transform.Translate( speed * Vector3.forward * Time.deltaTime);

		} // End if (range <= chaseRange)
        else
        {
            _agent.Resume();
            return;

        } // End else (if ( range <= chaseRange ))
		
	} // End function Update()

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "tp")
        {
            Debug.Log("je quitte le tp");
        }
    }

    void OnTriggerEnter(Collider target)
    {
       if(target.tag == "tp")
       {
           Debug.Log("j'entre dans le tp");
       }
    }
}

