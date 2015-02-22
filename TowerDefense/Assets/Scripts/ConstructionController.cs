using UnityEngine;
using System.Collections;

public class ConstructionController : MonoBehaviour
{
    //public Object Item;
    public TowerPoolManager TowerPoolManager;
    public int RangeTower = 6;
    private uint _hitCounter;
    private float _distance = 3.0f;
    private bool _construction = false;

    void Update()
    {
        if (_construction == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitTower;
            transform.position = new Vector3(1000, 1000, 1000);

          
            int layerMask = (1 << 8 | 1 << 9 | 1 << 10);
            layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hitTower, 100, layerMask))
            {
                transform.position = hitTower.point;
                 if(Input.GetMouseButtonDown(0) && _hitCounter == 0)
                {
                    var tower = TowerPoolManager.GetTower();
                    tower.gameObject.SetActive(true);
                    tower.Transform.position = transform.position;

                     //add box collider for shoot range
                    tower.RangeCollider.enabled = true;
					tower.RangeCollider.isTrigger = true;
                    //add box collider to tower
                    tower.OwnCollider.enabled = true;

                    Physics.IgnoreCollision(transform.collider, tower.RangeCollider);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
            foreach (var it in transform.gameObject.renderer.materials)
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
                 foreach (var it in transform.gameObject.renderer.materials)
                     it.color = Color.green;
             }
        }
    }

    public void Reset()
    {
        _hitCounter = 0;
        foreach (var it in transform.gameObject.renderer.materials)
            it.color = Color.green;
    }

    public void SetConstruction(bool contruction)
    {
        _construction = contruction;
    }

    public bool GetConstruction()
    {
        return _construction;
    }
}