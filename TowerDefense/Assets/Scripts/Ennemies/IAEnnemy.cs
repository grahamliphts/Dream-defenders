using UnityEngine;
using System.Collections;

public class IAEnnemy : MonoBehaviour 
{
	// This is the object to follow
	public Transform leader;  
	//var leader : Transform;
	
	// This is the object which follows
	//public Transform follower;  
	//var follower : Transform;

	//this is the arrival point
	public Transform ArrivalP;  

	private NavMeshAgent _agent;

	// This is the speed with which the follower will pursue
	float speed = 2f;
	
	// This is the range at which to pursue
	float chaseRange = 5f;
	
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
		
		if ( range <= chaseRange ){
			
			// If the follower is close enough to the leader, then chase!
			//Debug.Log(message:"in range");
			_agent.Stop();
			transform.LookAt(leader);
			transform.Translate( speed * Vector3.forward * Time.deltaTime);

			// launch fireball
			//GameObject fireballShoot = Instantiate(Fireball, SpawnPoint.transform.position, Quaternion.identity) as GameObject;
			//fireballShoot.rigidbody.AddForce(transform.forward * 1500);
			
		} // End if (range <= chaseRange)
		
		else {
			_agent.Resume();
			// The follower is out of range. Do nothing.
			return;
			
		} // End else (if ( range <= chaseRange ))
		
	} // End function Update()
}
