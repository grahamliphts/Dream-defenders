using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour 
{
    public Transform newtransform;
    public Rigidbody newrigidbody;
    public Collider owncollider;
    public Collider rangeCollider;


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
    public Collider RangeCollider
     {
         get
         {
             return rangeCollider;
         }
         set
         {
             rangeCollider = value;
         }
     }
    public Collider OwnCollider
     {
         get
         {
             return owncollider;
         }
         set
         {
             owncollider = value;
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
}
