using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyManager : MonoBehaviour 
{
    private int _numberMaxFire;
	private int _numberMaxPoison;
	private int _numberMaxIce;
	private int _numberMaxElec;

    [SerializeField]
	private int _numberElec;
	[SerializeField]
	private int _numberPoison;
	[SerializeField]
	private int _numberIce;
	[SerializeField]
	private int _numberFire;

    public Transform SpawnEnemy;
	public Transform ArrivalP;

    public EnemyPoolManager[] EnemyPools;
   
	public List<Transform> players;
	private int _nbTotalEnemies;
	enum Type {Elec, Fire, Ice, Poison};

	public void Start()
	{
		//SetMax - Nombre dans la pool
		_numberMaxFire = EnemyPools[0].enemies.Length;
		_numberMaxElec = EnemyPools[1].enemies.Length;
		_numberMaxPoison = EnemyPools[2].enemies.Length;
		_numberMaxIce = EnemyPools[3].enemies.Length;
	}
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
		for (int j = 0; j < players.Count; j++)
			enemy.iaEnemy.AddLeader(players[j]);
		
		enemy.newtransform.position = SpawnEnemy.position;
		enemy.gameObject.SetActive(true);
		enemy.iaEnemy.SetArrivalP(ArrivalP);
		enemy.agent.SetDestination(ArrivalP.position);
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
