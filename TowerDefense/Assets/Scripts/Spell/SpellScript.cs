using UnityEngine;
using System.Collections;

public class SpellScript : MonoBehaviour 
{
    public Transform newtransform;
    public Rigidbody newrigidbody;

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

    void OnCollisionEnter(Collision col)
    {
		Debug.Log(col.gameObject.name);
		transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		transform.localPosition = new Vector3(0, 0, 0);
		//StartCoroutine("Disable");
       
    }

	IEnumerator Disable()
	{
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
		transform.gameObject.SetActive(false);
	}
}
