using UnityEngine;
using System.Collections;

public class MonsterLifeManager : MonoBehaviour 
{
    public int life;
    public string[] _tag;
    public int[] _damage;
    private int life_save;

    void start()
    {
        life_save = life;
    }

    void OnCollisionEnter(Collision col)
    {
        int count = 0;
        foreach (string element in _tag)
        {
            if (col.gameObject.tag == element)
                life = life - _damage[count];
            count++;
        }
        if(life <= 0)
        {
            life = life_save;
            transform.position = new Vector3(1000,1000, 1000);
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            Destroy(agent);
            transform.gameObject.SetActive(false);
        }
    }
}
