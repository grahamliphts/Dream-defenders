using UnityEngine;
using System.Collections;

public class FrozenTowerScript : MonoBehaviour 
{
    public Transform transform;
    public Rigidbody rigidbody;
    public Collider collider;
    public Collider rangeCollider;
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
             return collider;
         }
         set
         {
             collider = value;
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
