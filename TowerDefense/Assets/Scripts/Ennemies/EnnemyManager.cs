using UnityEngine;
using System.Collections;

public class EnnemyManager : MonoBehaviour {

    [SerializeField]
    int number;
    [SerializeField]
    int damage;

    public Transform SpawnEnemy;
    public EnemyPoolManager EnemyPoolManager;
    public Transform ArrivalP;
	void Start () 
    {
	    for(int i = 0; i < number; i++)
        {
            var enemy = EnemyPoolManager.GetEnemy();
            enemy.Transform.position = SpawnEnemy.position;
            enemy.Agent = enemy.gameObject.AddComponent<NavMeshAgent>();
            //TODO Remove GetComponent
            enemy.GetComponent<IAEnemy>().SetAgent(enemy.Agent);
            enemy.Transform.position = SpawnEnemy.position;
            enemy.gameObject.SetActive(true);
        }
	}
}
