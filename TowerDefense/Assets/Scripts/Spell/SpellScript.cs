using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour 
{
    public Transform transform;
    public Rigidbody rigidbody;

    public Transform Transform
    {
        get
        {
            return transform;
        }
        set
        {
            transform = value;
        }
    }
    public Rigidbody Rigidbody
    {
        get
        {
            return rigidbody;
        }
        set
        {
            rigidbody = value;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != "fireball")
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.transform.position = new Vector3(0, 0, 0);
            transform.gameObject.SetActive(false);
        }
    }
}
