using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
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
}
