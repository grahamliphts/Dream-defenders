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
	private int _numberFire;
    [SerializeField]
	private int _numberElec;
	[SerializeField]
	private int _numberPoison;
	[SerializeField]
	private int _numberIce;

    public Transform SpawnEnemy;
	public Transform ArrivalP;

    public EnemyPoolManager[] EnemyPools;
   
	[SerializeField]
	private List<Transform> _players;
	public List<Transform> players
	{
		set
		{
			_players = value;
		}
		get
		{
			return _players;
		}
	}
	private int _nbTotalEnemies;
	enum Type {Fire, Elec, Poison, Ice};

	private NetworkView _networkView;

	public void Start()
	{
		//SetMax - Nombre dans la pool
		_numberMaxFire = EnemyPools[0].enemies.Length;
		_numberMaxElec = EnemyPools[1].enemies.Length;
		_numberMaxPoison = EnemyPools[2].enemies.Length;
		_numberMaxIce = EnemyPools[3].enemies.Length;

		_networkView = GetComponent<NetworkView>();
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

		if(LevelStart.instance.modeMulti)
			_networkView.RPC("ResetIndexEnemies", RPCMode.All);
		else
		{
			for (int i = 0; i < 4; i++)
				EnemyPools[i].ResetIndex();
		}
	}

	public void InitEnemy(int index)
	{
		if (!LevelStart.instance.modeMulti || Network.isServer)
		{
			if (LevelStart.instance.modeMulti)
				_networkView.RPC("SetEnemy", RPCMode.All, (index));
			else
				SpawnEnemies(index);	
		}
	}

	[RPC]
	private void ResetIndexEnemies()
	{
		for (int i = 0; i < 4; i++)
			EnemyPools[i].ResetIndex();
	}

	[RPC]
	private void SetEnemy(int index)
	{
		SpawnEnemies(index);
	}

	private void SpawnEnemies(int index)
	{
		var enemy = EnemyPools[index].GetEnemy();
		for (int j = 0; j < _players.Count; j++)
			enemy.iaEnemy.AddLeader(_players[j]);

		enemy.newtransform.position = SpawnEnemy.position;
		enemy.gameObject.SetActive(true);
		enemy.iaEnemy.SetArrivalP(ArrivalP);

		if (!LevelStart.instance.modeMulti || Network.isServer)
			enemy.agent.SetDestination(ArrivalP.position);
		else
			enemy.agent.enabled = false;

		
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

	public void SetNetworkView(NetworkView networkView)
	{
		_networkView = networkView;
	}
}
