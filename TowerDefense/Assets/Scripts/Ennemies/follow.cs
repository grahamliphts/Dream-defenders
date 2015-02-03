using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {

	public Transform leader ;  
	//var leader : Transform;
	float speed = 3f; // The speed of the follower
	
	
	void Update(){
		transform.LookAt(leader);
		transform.Translate(speed*Vector3.forward*Time.deltaTime);
	}
}
