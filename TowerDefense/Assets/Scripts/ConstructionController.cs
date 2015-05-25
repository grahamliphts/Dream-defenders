using UnityEngine;
using System.Collections;

public class ConstructionController : MonoBehaviour
{
    public int RangeTower = 6;
    private int _hitCounter;
	private bool _construction;

    void Update()
    {
        if (_construction == true && LoopManager.modeConstruction)
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
	public bool GetConstruction()
	{
		return _construction;
	}

	public void SetConstruction(bool construction)
	{
		_construction = construction;
	}

	public int GetHitCounter()
	{
		return _hitCounter;
	}
}