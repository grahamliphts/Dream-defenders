using UnityEngine;
using System.Collections;

public class NexusLife : MonoBehaviour {

	[SerializeField]
	private float _life;
	[SerializeField]
	private int damage;

	void Start()
	{
		_life = 100;
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log(col.gameObject.tag);
		if (_life > 0)
			_life = _life - damage;

		Debug.Log(_life);
	}

	public float GetLife()
	{
		return _life;
	}
}