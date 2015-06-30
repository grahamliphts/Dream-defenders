using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour 
{
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
	[SerializeField]
	private Rigidbody _rigidbody;
	public Rigidbody mrigidbody
    {
        get
        {
			return _rigidbody;
        }
        set
        {
			_rigidbody = value;
        }
    }
	[SerializeField]
	private ParticleSystem _particle;
	[SerializeField]
	private float _degats;
	public float degats
	{
		set
		{
			_degats = value;
		}
		get
		{
			return _degats;
		}
	}

	private SpellScript _spellScript;
	public SpellScript spellScript
	{
		get
		{
			return _spellScript;
		}
		set
		{
			_spellScript = value;
		}
	}

	void Start()
	{
		_spellScript = this;
	}
    void OnCollisionEnter(Collision col)
    {
		_rigidbody.velocity = new Vector3(0, 0, 0);
		_transform.localPosition = new Vector3(0, 0, 0);
		if (_particle != null)
			StartCoroutine("Disable");
		else
			transform.gameObject.SetActive(false);
       
    }

	IEnumerator Disable()
	{
		yield return new WaitForSeconds(_particle.duration);
		transform.gameObject.SetActive(false);
	}
}