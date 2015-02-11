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
            rigidbody.velocity = new Vector3(0, 0, 0);
            transform.transform.position = new Vector3(1000, 1000, 1000);
            transform.gameObject.SetActive(false);
    }
}
