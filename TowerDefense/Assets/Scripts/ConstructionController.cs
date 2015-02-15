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
            //Debug.Log(_construction);
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

            //TODO: collider only with own collider of towers already places
            int layerMask = 1 << 9;
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
           
            transform.gameObject.renderer.material.color = Color.red;
            _hitCounter++;
         //   Debug.Log("Trigger Enter" + transform.gameObject + " " + _hitCounter);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
           
             _hitCounter--;
        //     Debug.Log("Trigger exit" + transform.gameObject + " " + _hitCounter);
            if(_hitCounter == 0)
                transform.gameObject.renderer.material.color = Color.green;
           
        }
    }

    public void SetConstruction(bool contruction)
    {
        _construction = contruction;
    }
}