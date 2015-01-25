using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour 
{
    void OnCollisionEnter(Collision other)
    {
         Destroy(transform.gameObject);
    }
}
