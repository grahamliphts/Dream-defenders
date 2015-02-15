using UnityEngine;
using System.Collections;

public class EnnemyManager : MonoBehaviour {

    [SerializeField]
    int number;
    [SerializeField]
    int damage;

    public Transform SpawnEnemy;
    public EnemyPoolManager EnemyPoolManager;
	void Start () 
    {
	    for(int i = 0; i <= number; i++)
        {
            var enemy = EnemyPoolManager.GetEnemy();
            enemy.Transform.position = SpawnEnemy.position;
        }
	}
	
	void Update () 
    {
	
	}
}
