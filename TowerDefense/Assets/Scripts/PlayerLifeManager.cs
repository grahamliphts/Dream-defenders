﻿using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoBehaviour
{
    private float _life;
    public string[] Tag;
    public int[] Damage;

    void Start()
    {
        _life = 100;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        int count = 0;
        if (_life > 0)
        {
            foreach (string element in Tag)
            {
                if (col.gameObject.tag == element)
                {
                    Debug.Log(_life);
                    _life = _life - Damage[count];

                }
                count++;
            }
        }
    }

    public float GetLife()
    {
        return _life;
    }
}
