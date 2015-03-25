using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour 
{
    public Transform newtransform;
    public Rigidbody newrigidbody;

    public Transform Transform
    {
        get
        {
            return newtransform;
        }
        set
        {
            newtransform = value;
        }
    }
    public Rigidbody Rigidbody
    {
        get
        {
            return newrigidbody;
        }
        set
        {
            newrigidbody = value;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.transform.position = new Vector3(0, 0, 0);
        transform.gameObject.SetActive(false);
		Debug.Log("set active false");
		Debug.Log(gameObject.name);
    }
}
