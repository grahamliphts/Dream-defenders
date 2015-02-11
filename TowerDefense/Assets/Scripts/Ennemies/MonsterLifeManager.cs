﻿using UnityEngine;
using System.Collections;

public class MonsterLifeManager : MonoBehaviour {

    public int life;
    public string[] _tag;
    public int[] _damage;
    public Rigidbody _body;
    
    void OnTriggerEnter(Collider target)
    {
        int count = 0;
        foreach (string element in _tag)
        {
            if (target.tag == element)
            {
                Debug.Log(_damage[count]);
                life = life - _damage[count];
            }
            count++;
        }
        if(life <= 0)
        {
            Debug.Log("Monster died");
            //TODO:POOL ENNEMY
            transform.position = new Vector3(1000,1000, 1000);
           // transform.gameObject.SetActive(false);
            
        }
       // Debug.Log(target.tag);
       // Debug.Log("trigger enter");
    }

}
