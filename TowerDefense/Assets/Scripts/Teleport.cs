using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject nextPoint;
    public GameObject prevPoint;
    private uint _count = 0;

	void OnTriggerEnter(Collider other)
    {
        if (nextPoint != null)
            other.transform.position = nextPoint.transform.position + other.rigidbody.velocity;

        if (prevPoint != null)
            other.transform.position = prevPoint.transform.position + other.rigidbody.velocity;
    }
}
