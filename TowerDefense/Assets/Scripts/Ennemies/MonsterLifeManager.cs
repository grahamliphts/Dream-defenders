using UnityEngine;
using System.Collections;

public class MonsterLifeManager : MonoBehaviour 
{
    private float _life;
	[SerializeField]
	private float _lifeMax;

    public string[] _tag;
    public int[] _damage;
    public Material materialModel;
	private Material _matHeathBar;
    public GameObject healthBar;

    void Start()
    {
		healthBar.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialModel);
		_matHeathBar = healthBar.GetComponent<Renderer>().material;
		_matHeathBar.SetFloat("_HP", _lifeMax);
		_life = _lifeMax;
    }

    void OnCollisionEnter(Collision col)
    {
        int count = 0;
        foreach (string element in _tag)
        {
            if (col.gameObject.tag == element)
			{
				_life = _life - _damage[count];
				_matHeathBar.SetFloat("_HP", _life);
				SetColorLife(_life);
			}
               
            count++;
        }
        if(_life <= 0)
        {
			//WHHAT TO DO AFTER ??
            //_life = life_save;
            transform.position = new Vector3(1000,1000, 1000);
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            Destroy(agent);
            transform.gameObject.SetActive(false);
        }
    }

	private void SetColorLife(float life)
	{
		if(life <= _lifeMax/2 && life >= _lifeMax/4)
			_matHeathBar.SetColor("_Color", Color.yellow);
		if (life <= _lifeMax / 4)
			_matHeathBar.SetColor("_Color", Color.red);
	}
}
