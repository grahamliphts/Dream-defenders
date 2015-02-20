using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject nextPoint;
    public GameObject prevPoint;
    private float count = 0;

	void OnTriggerEnter(Collider other)
    {
        if (other.tag != "ennemy")
        {
            if (nextPoint != null)
            {
                nextPoint.collider.enabled = false;
                other.transform.position = nextPoint.transform.position;
              
                nextPoint.collider.enabled = true;
            }
            if (prevPoint != null)
                other.transform.position = prevPoint.transform.position;// +other.rigidbody.velocity;
        }
        else
        {
            other.gameObject.SetActive(false);   
            if (nextPoint != null)
            {
                nextPoint.collider.enabled = false;
                other.transform.position = nextPoint.transform.position;

                nextPoint.collider.enabled = true;
            }
                
            other.gameObject.SetActive(true);
        }
    }

}
