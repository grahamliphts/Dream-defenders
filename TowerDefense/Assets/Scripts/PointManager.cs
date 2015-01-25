﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointManager : MonoBehaviour
{
    public List<GameObject> point_list;
    public GameObject TpParticle;

    void Start()
    {
        for(int i = 0; i < point_list.Count; i++)
        {
            point_list[i].AddComponent<SphereCollider>();
            SphereCollider collider = point_list[i].GetComponent<SphereCollider>();
            collider.isTrigger = true;
            point_list[i].AddComponent<Teleport>();
            Teleport teleport = point_list[i].GetComponent<Teleport>();
            Instantiate(TpParticle, point_list[i].transform.position, Quaternion.identity);

            if (i > 1)
                teleport.prevPoint = point_list[i - 1];
   
            if(i < point_list.Count - 1 && (i%2) == 1)
                teleport.nextPoint = point_list[i + 1];
        }
    }
}
