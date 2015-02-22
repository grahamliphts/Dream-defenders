using UnityEngine;
using System.Collections;

public class EnnemyManager : MonoBehaviour 
{
    [SerializeField]
    private int _numberMax;
    [SerializeField]
    private int _number;
    [SerializeField]
    private int _damage;

    public Transform SpawnEnemy;
    public EnemyPoolManager EnemyPoolManager;
    public Transform ArrivalP;
    public PointManager ManagerPoint;

	public void Spawn() 
    {
        for (int i = 0; i < _number; i++)
        {
            var enemy = EnemyPoolManager.GetEnemy();
            enemy.Transform.position = SpawnEnemy.position;

            enemy.Agent = enemy.gameObject.AddComponent<NavMeshAgent>();
            enemy.GetComponent<IAEnemy>().SetAgent(enemy.Agent);
            enemy.Transform.position = SpawnEnemy.position;
            enemy.gameObject.SetActive(true);
            enemy.Agent.SetDestination(ArrivalP.position);
        }

        EnemyPoolManager.ResetIndex();
	}

    public bool AllDied()
    {
        int numberEnemies = EnemyPoolManager.NumberEnemiesActive();
        if (numberEnemies == 0)
            return true;
        else
            return false;
    }

    public void AddEnemies(int number)
    {
        _number += number;
        if (_number > _numberMax)
            Debug.Log("No more enemies");
    }

}
