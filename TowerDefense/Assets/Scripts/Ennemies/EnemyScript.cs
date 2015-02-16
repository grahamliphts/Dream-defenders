using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public Transform transform;
    public Rigidbody rigidbody;
    public NavMeshAgent agent;

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

    public NavMeshAgent Agent
    {
         get
        {
            return agent;
        }
        set
        {
            agent = value;
        }
    }
}
