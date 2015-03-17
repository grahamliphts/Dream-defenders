﻿using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public Transform newtransform;
    public Rigidbody newrigidbody;
    public NavMeshAgent agent;

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
