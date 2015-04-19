using UnityEngine;
using System.Collections;

public class ConstructionController : MonoBehaviour
{
    public int RangeTower = 6;
    private uint _hitCounter;
	private bool _construction;

    void Update()
    {
        if (_construction == true && LoopManager.modeConstruction)
        {
            Vector3 pos = new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0.0f);
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitTower;
            transform.position = new Vector3(1000, 1000, 1000);

            int layerMask = (1 << 8 | 1 << 9);
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
            foreach (var it in transform.gameObject.GetComponent<Renderer>().materials)
                it.color = Color.red;
            _hitCounter++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
             _hitCounter--;
             if (_hitCounter == 0)
             {
                 foreach (var it in transform.gameObject.GetComponent<Renderer>().materials)
                     it.color = Color.green;
             }
        }
    }

    public void Reset()
    {
        _hitCounter = 0;
        foreach (var it in transform.gameObject.GetComponent<Renderer>().materials)
            it.color = Color.green;
    }

	public bool GetConstruction()
	{
		return _construction;
	}

	public void SetConstruction(bool construction)
	{
		_construction = construction;
	}

	public uint GetHitCounter()
	{
		return _hitCounter;
	}
}