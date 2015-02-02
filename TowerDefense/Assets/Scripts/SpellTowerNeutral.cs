using UnityEngine;
using System.Collections;

public class SpellTowerNeutral : MonoBehaviour 
{

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ennemy")
        {
            Debug.Log("Shoot ennemy");
        }
    }
}
