using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject nextPoint;
    public GameObject nextPointDest;

    public GameObject prevPoint;

    public GameObject _tpPoint;

	void OnTriggerEnter(Collider other)
    {
        if (other.tag != "ennemy")
        {
            if (nextPoint != null)
                other.transform.position = nextPoint.transform.position + nextPoint.transform.forward * 3;

            if (prevPoint != null)
                other.transform.position = prevPoint.transform.position + prevPoint.transform.forward * 3;
        }
        else
        {
            NavMeshAgent agent;
            agent = other.transform.GetComponent<NavMeshAgent>();
            other.gameObject.SetActive(false);
            if (nextPoint != null)
                other.transform.position = nextPoint.transform.position + nextPoint.transform.forward * 3;
            other.gameObject.SetActive(true);

            if(nextPoint != null && nextPointDest != null)
                agent.SetDestination(nextPointDest.transform.position);

            if (nextPoint == null)
            {
                Debug.Log("Touch Nexus");
            }
        }
    }

    public GameObject GetGameObject()
    {
        return transform.gameObject;
    }
}
