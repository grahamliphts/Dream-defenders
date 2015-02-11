using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoBehaviour
{
    public int life;
    public string[] _tag;
    public int[] _damage;

    void OnTriggerStay(Collider target)
    {
        int count = 0;
        if (life >= 0)
            foreach (string element in _tag)
            {
                if (target.tag == element)
                {
                    Debug.Log(_damage[count]);
                    life = life - _damage[count];
                }
                count++;
            }
        else
        {
            Debug.Log("player died");
            //Destroy(this.gameObject);
        }
        // Debug.Log(target.tag);
        // Debug.Log("trigger enter");
    }
}
