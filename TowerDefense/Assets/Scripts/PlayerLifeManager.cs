using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    private float _life;
    public string[] _tag;
    public int[] _damage;

    void OnTriggerEnter(Collider target)
    {
        int count = 0;
        if (_life >= 0)
        {
            foreach (string element in _tag)
            {
                if (target.tag == element)
                {
                    Debug.Log(_damage[count]);
                    _life = _life - _damage[count];
                }
                count++;
            }
        }
        else
            transform.gameObject.SetActive(false);
    }

    public float GetLife()
    {
        return _life;
    }
}
