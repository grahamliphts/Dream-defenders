using UnityEngine;
using System.Collections;

public class WalkAhead : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position.Set (transform.position.x, transform.position.y, transform.position.z+1);
		//transform.localPosition.Set (transform.position.x, transform.position.y, transform.position.z + 1);
		transform.Translate(-Vector3.forward * Time.deltaTime);
	
	}
}
