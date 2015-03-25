using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public List<Transform> players;

	public void Spawn() 
    {
        for (int i = 0; i < _number; i++)
        {
            var enemy = EnemyPoolManager.GetEnemy();
            enemy.newtransform.position = SpawnEnemy.position;

            enemy.Agent = enemy.gameObject.AddComponent<NavMeshAgent>();
			IAEnemy iaEnnemy = enemy.GetComponent<IAEnemy>();
			//Debug.Log("add " + players.Count + "leader to ennemy" + enemy.gameObject.name);
			for (int j = 0; j < players.Count; j++)
				iaEnnemy.AddLeader(players[j]);
            iaEnnemy.SetAgent(enemy.Agent);
            enemy.Transform.position = SpawnEnemy.position;
            enemy.gameObject.SetActive(true);
			Debug.Log(ArrivalP.position);
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
