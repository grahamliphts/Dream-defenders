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
    public PointManager ManagerPoint;
    private int i;

	void Start () 
    {
	    for(i = 0; i < number; i++)
        {
            var enemy = EnemyPoolManager.GetEnemy();
            enemy.Transform.position = SpawnEnemy.position;
            enemy.Agent = enemy.gameObject.AddComponent<NavMeshAgent>();

            enemy.GetComponent<IAEnemy>().SetAgent(enemy.Agent);
            enemy.Transform.position = SpawnEnemy.position;
            enemy.gameObject.SetActive(true);
            Debug.Log(i + " " + number);
        }
	}

    public int CurrentNumber
    {
        get { return number; }
        set { number = value; }
    }

    public bool AllSpawned
    {
       
        get
        {
            if(i == number)
                return true;
            else
                return false;
        }
    }

     public bool AllDied
    {

        get
        {
            /*if(<is all died>)
              return true;
          else
              return false;*/
            return true;

        }
    }

}
