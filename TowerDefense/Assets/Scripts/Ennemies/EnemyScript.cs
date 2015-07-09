using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
	[SerializeField]
	private MonsterLifeManager _monsterLifeManager;
	public MonsterLifeManager monsterLifeManager
	{
		get
		{
			return _monsterLifeManager;
		}
		set
		{
			_monsterLifeManager = value;
		}
	}
	[SerializeField]
    private NavMeshAgent _agent;
	public NavMeshAgent agent
	{
		get
		{
			return _agent;
		}
		set
		{
			_agent = value;
		}
	}
	[SerializeField]
	private IAEnemy _iaEnemy;
	public IAEnemy iaEnemy
	{
		get
		{
			return _iaEnemy;
		}
		set
		{
			_iaEnemy = value;
		}
	}
	[SerializeField]
	private Transform _transform;
    public Transform mtransform
    {
        get
        {
            return _transform;
        }
        set
        {
            _transform = value;
        }
    }
}
