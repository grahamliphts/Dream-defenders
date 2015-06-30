using UnityEngine;
using System.Collections;

public class ConstructionController : MonoBehaviour
{
	private bool _construction;
	private int _hitCounter;
	public int hitCounter
	{
		set
		{
			_hitCounter = value;
		}
		get
		{
			return _hitCounter;
		}
	}

    void Update()
    {
        if (LoopManager.modeConstruction)
        {
            Vector3 pos = new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0.0f);
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitTower;

            int layerMask = (1 << 8 | 1 << 9 | 1 << 10);
			layerMask = ~layerMask;

            if (Physics.Raycast(ray, out hitTower, 100, layerMask))
            {
                transform.position = hitTower.point;
			}
		}
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
			SetColorChild(Color.red);
            _hitCounter++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
             _hitCounter--;
             if (_hitCounter <= 0)
			 {
				 _hitCounter = 0;
				 SetColorChild(Color.green);
			 }
        }
    }

    public void Reset()
    {
        _hitCounter = 0;
		SetColorChild(Color.green);
    }

	public void SetColorChild(Color color)
	{
		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild(i).GetComponent<Renderer>().material.color = color;
	}
}