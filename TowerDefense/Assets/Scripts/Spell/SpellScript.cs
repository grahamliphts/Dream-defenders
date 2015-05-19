using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour 
{
    public Transform newtransform;
    public Rigidbody newrigidbody;
	public ParticleSystem particle;

    public Transform Transform
    {
        get
        {
            return newtransform;
        }
        set
        {
            newtransform = value;
        }
    }
    public Rigidbody Rigidbody
    {
        get
        {
            return newrigidbody;
        }
        set
        {
            newrigidbody = value;
        }
    }

	public ParticleSystem Particle
	{
		get
		{
			return particle;
		}
		set
		{
			particle = value;
		}
	}
    void OnCollisionEnter(Collision col)
    {

		newrigidbody.velocity = new Vector3(0, 0, 0);
		newtransform.localPosition = new Vector3(0, 0, 0);
		if (particle != null)
			StartCoroutine("Disable");
		else
			transform.gameObject.SetActive(false);
       
    }

	IEnumerator Disable()
	{
		yield return new WaitForSeconds(particle.duration);
		transform.gameObject.SetActive(false);
	}
}