using UnityEngine;
using System.Collections;

public class IAEnnemy : MonoBehaviour 
{
     public Transform Target;    
     public Transform Projectile;
 
     public float MaximumLookDistance = 15;
     public float MaximumAttackDistance = 10;
     public float FollowSpeed = 5;
     public float MinimumDistanceFromPlayer = 2;
 
     public float RotationDamping = 6;
 
     void Update()  
     {
 
         var distance = Vector3.Distance(Target.position, transform.position);
 
         if(distance <= MaximumLookDistance) {
             LookAtTarget ();
 
             if(distance <= MaximumAttackDistance)
                 AttackAndFollowTarget (distance);
         }   
         else
             renderer.material.color = Color.green; 
     }
 
 
     void LookAtTarget () 
     {
         renderer.material.color = Color.yellow;
         var dir = Target.position - transform.position;
         var rotation = Quaternion.LookRotation(dir);
         transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationDamping);
     }
 
 
     void AttackAndFollowTarget (float distance) 
{
         renderer.material.color = Color.red;
         if(distance > MinimumDistanceFromPlayer)
         
             transform.Translate((Target.position - transform.position).normalized * FollowSpeed * Time.deltaTime);
         Debug.Log((Target.position - transform.position).normalized * FollowSpeed * Time.deltaTime);
        // Instantiate(Projectile, transform.position + (Target.position - transform.position).normalized, Quaternion.LookRotation(Target.position - transform.position));
     }
}
