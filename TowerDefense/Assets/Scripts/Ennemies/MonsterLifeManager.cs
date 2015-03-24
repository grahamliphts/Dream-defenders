using UnityEngine;
using System.Collections;

public class MonsterLifeManager : MonoBehaviour 
{
    public int life;
    public string[] _tag;
    public int[] _damage;
    private int life_save;
    public Material material;
    public GameObject healthBar;

    void Start()
    {
        //Debug.Log(material.shader);
        healthBar.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(material);
        //Debug.Log(healthBar.GetComponent<Renderer>().material.shader);
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
