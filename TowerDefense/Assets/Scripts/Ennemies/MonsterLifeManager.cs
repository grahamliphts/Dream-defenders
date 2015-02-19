using UnityEngine;
using System.Collections;

public class MonsterLifeManager : MonoBehaviour 
{
    public int life;
    public string[] _tag;
    public int[] _damage;
    
    void OnCollisionEnter(Collision target)
    {
        int count = 0;
        foreach (string element in _tag)
        {
            if (target.gameObject.tag == element)
            {
               // Debug.Log(_damage[count]);
                life = life - _damage[count];
            }
            count++;
        }
        if(life <= 0)
        {
            Debug.Log(target.gameObject.name);
            //Probleme : cannot set active (false) because of the on trigger exit of tower
            transform.position = new Vector3(1000,1000, 1000);
            //transform.gameObject.SetActive(false);
        }
    }
}
