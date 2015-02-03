using UnityEngine;
using System.Collections;

public class GoToPoint : MonoBehaviour {

		public Transform Target;  
		private NavMeshAgent _agent;
		
		void Start ()
		{
	//_agent = GetComponent<navmeshagent>();
			_agent = GetComponent<NavMeshAgent>();
			_agent.SetDestination(Target.position);
		}
		
		void Update ()
		{
			//_agent.SetDestination(Target.position);
		}
}
