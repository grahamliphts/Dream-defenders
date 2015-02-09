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

    void Start()
    {
        _hitCounter = 0;
        transform.gameObject.renderer.material.color = Color.green;
    }
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            Debug.Log(_construction);
            if (_construction == false)
            {
                _construction = true;
                transform.gameObject.SetActive(true);
            }

            else
            {
                transform.position = new Vector3(1000, 1000, 1000);
                _construction = false;
            }
        }

        if (_construction == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitTower;
            transform.position = new Vector3(1000, 1000, 1000);

            if (Physics.Raycast(ray, out hitTower, 100))
            {
                transform.position = hitTower.point;
                 if(Input.GetMouseButtonDown(0) && _hitCounter == 0)
                {
                    var tower = TowerPoolManager.GetFrozenTower();
                    tower.gameObject.SetActive(true);
                    tower.Transform.position = transform.position;
                    Debug.Log(transform.position);
                     //add box collider for shoot range
                    tower.RangeCollider.enabled = true;
					tower.RangeCollider.isTrigger = true;
                    //add box collider to tower
                    tower.OwnCollider.enabled = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
            transform.gameObject.renderer.material.color = Color.red;
            _hitCounter++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
             _hitCounter--;
            if(_hitCounter == 0)
                transform.gameObject.renderer.material.color = Color.green;
           
        }
    }

    public void SetConstruction(bool contruction)
    {
        _construction = contruction;
    }
}