using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject nextPoint;
    public GameObject nextPointDest;

    public GameObject prevPoint;

    public GameObject _tpPoint;

	private static int _nbFloor;

	void Start()
	{
		_nbFloor = 0;
	}
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
			SoundManager.instance.PlayTpCrossing();
			if (nextPoint != null)
			{
				other.transform.position = nextPoint.transform.position + nextPoint.transform.forward * 3;
				_nbFloor++;
			}
            if (prevPoint != null)
			{
				other.transform.position = prevPoint.transform.position + prevPoint.transform.forward * 3;
				_nbFloor--;
			}
        }

        else if(other.tag == "ennemy")
        {
			if (!LevelStart.instance.modeMulti || Network.isServer)
			{
				NavMeshAgent agent;
				agent = other.transform.GetComponent<NavMeshAgent>();

				other.gameObject.SetActive(false);
				if (nextPoint != null)
					other.transform.position = nextPoint.transform.position + nextPoint.transform.forward * 3;
				other.gameObject.SetActive(true);
				if (nextPoint != null && nextPointDest != null)
					agent.SetDestination(nextPointDest.transform.position);
			}
		}
    }

    public GameObject GetGameObject()
    {
        return transform.gameObject;
    }

	public int GetNbFloor()
	{
		return _nbFloor;
	}
}
