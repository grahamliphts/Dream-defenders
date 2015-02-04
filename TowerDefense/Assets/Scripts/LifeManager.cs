using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    public int life;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
	
	}
    void OnTriggerStay(Collider target)
    {
        
        if (target.tag == "Fireball")
        {
            Debug.Log("Lost 5 pv");
            life = life - 5;
           // Destroy(this.gameObject);
        }
        if(life == 0)
        {
            Debug.Log("Monster died");
            Destroy(this.gameObject);
        }
        Debug.Log(target.tag);
        Debug.Log("trigger enter");
    }

}
