using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour 
{

    public GameObject Fireball;
    public Transform SpawnPoint;

	void Start () 
    {
	
	}
	

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject fireballShoot = Instantiate(Fireball, SpawnPoint.transform.position, Quaternion.identity) as GameObject;
            fireballShoot.rigidbody.AddForce(transform.forward * 1000);
        }

    }
}
