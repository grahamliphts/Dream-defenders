using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyManager : MonoBehaviour 
{
    [SerializeField]
    private int _numberMaxFire;
	[SerializeField]
	private int _numberMaxPoison;
	[SerializeField]
	private int _numberMaxIce;
	[SerializeField]
	private int _numberMaxElec;

    [SerializeField]
	private int _numberElec;
	[SerializeField]
	private int _numberPoison;
	[SerializeField]
	private int _numberIce;
	[SerializeField]
	private int _numberFire;

	[SerializeField]
    public Transform SpawnEnemy;


    public EnemyPoolManager[] EnemyPools;
    public Transform ArrivalP;
	public List<Transform> players;
	private int _nbTotalEnemies;
	enum Type {Elec, Fire, Ice, Poison};

	public void Spawn() 
    {
		for (int i = 0; i < _numberElec; i++)
			InitEnemy((int)Type.Elec);
		for (int i = 0; i < _numberFire; i++)
			InitEnemy((int)Type.Fire);
		for (int i = 0; i < _numberIce; i++)
			InitEnemy((int)Type.Ice);
		for (int i = 0; i < _numberPoison; i++)
			InitEnemy((int)Type.Poison);

		for(int i = 0; i < 4; i++)
			EnemyPools[i].ResetIndex();
	}

	public void InitEnemy(int index)
	{
		var enemy = EnemyPools[index].GetEnemy();
		enemy.newtransform.position = SpawnEnemy.position;
		//enemy.Agent = enemy.gameObject.GetComponent<NavMeshAgent>(); TODO
		enemy.Agent = enemy.gameObject.AddComponent<NavMeshAgent>();
		IAEnemy iaEnnemy = enemy.GetComponent<IAEnemy>();
		//Debug.Log("add " + players.Count + "leader to ennemy" + enemy.gameObject.name);
		for (int j = 0; j < players.Count; j++)
			iaEnnemy.AddLeader(players[j]);
		iaEnnemy.SetAgent(enemy.Agent);
		enemy.Transform.position = SpawnEnemy.position;
		enemy.gameObject.SetActive(true);
		iaEnnemy.SetArrivalP(ArrivalP);
		enemy.Agent.SetDestination(ArrivalP.position);
	}

    public bool AllDied()
    {
		int numberEnemies = 0;
		for(int i = 0; i < 4; i++)
		{
			numberEnemies += EnemyPools[(int)Type.Elec].NumberEnemiesActive()
				+ EnemyPools[(int)Type.Fire].NumberEnemiesActive()
				+ EnemyPools[(int)Type.Ice].NumberEnemiesActive()
				+ EnemyPools[(int)Type.Poison].NumberEnemiesActive();
		}
        if (numberEnemies == 0)
            return true;
        else
            return false;
    }

    public void AddEnemiesElec(int number)
    {
        _numberElec += number;
		if (_numberElec > _numberMaxElec)
            Debug.Log("No more enemies");
    }

	public void AddEnemiesIce(int number)
	{
		_numberIce += number;
		if (_numberIce > _numberMaxIce)
			Debug.Log("No more enemies");
	}
	public void AddEnemiesFire(int number)
	{
		_numberFire += number;
		if (_numberFire > _numberMaxFire)
			Debug.Log("No more enemies");
	}

	public void AddEnemiesPoison(int number)
	{
		_numberPoison += number;
		if (_numberPoison > _numberMaxPoison)
			Debug.Log("No more enemies");
	}
}
