using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject nextPoint;
    public GameObject prevPoint;
    private uint _count = 0;

	void OnTriggerEnter(Collider other)
    {
        if (other.tag != "ennemy")
        {
            if (nextPoint != null)
                other.transform.position = nextPoint.transform.position + other.rigidbody.velocity;

            if (prevPoint != null)
                other.transform.position = prevPoint.transform.position + other.rigidbody.velocity;
        }
        else
        {
            Debug.Log("un fantôme sa barre !!!");
            other.gameObject.SetActive(false);   
            if (nextPoint != null)
               other.transform.position = new Vector3(nextPoint.transform.position.x - 2, nextPoint.transform.position.y, nextPoint.transform.position.z - 2);

            if (prevPoint != null)
                other.transform.position = new Vector3(prevPoint.transform.position.x - 2,prevPoint.transform.position.y,prevPoint.transform.position.z - 2 )    ;
            other.gameObject.SetActive(true);
        }
    }
}
