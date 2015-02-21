using UnityEngine;
using System.Collections;

public class MonsterLifeManager : MonoBehaviour 
{
    public int life;
    public string[] _tag;
    public int[] _damage;

    private int life_save;
    void start()
    {
        life_save = life;
       // Debug.Log(life_save);
    }
    void OnCollisionEnter(Collision col)
    {
        int count = 0;
        foreach (string element in _tag)
        {
            if (col.gameObject.tag == element)
            {
               // Debug.Log(col.gameObject.tag);
                life = life - _damage[count];
            }
            count++;
        }
        if(life <= 0)
        {
           // Debug.Log(col.gameObject.name);
            //Probleme : cannot set active (false) because of the on trigger exit of tower
            life = life_save;
            //Debug.Log(life_save);
            transform.position = new Vector3(1000,1000, 1000);
            //transform.gameObject.SetActive(false);
        }
    }
}
