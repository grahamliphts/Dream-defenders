using UnityEngine;
using System.Collections;

public class ConstructionController : MonoBehaviour
{
    public Object item;
    public int RangeTower = 6;
    private uint _hitCounter;
    private float _distance = 3.0f;
    private bool _construction = false;

    void Start()
    {
        _hitCounter = 0;
        transform.gameObject.renderer.material.color = Color.green;
    }

    void FixedUpdate()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            Debug.Log(_construction);
            if (_construction == false)
                _construction = true;
            else
            {
                transform.position = new Vector3(1000, 1000, 1000);
                _construction = false;
            }
        }

        if (_construction == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
            RaycastHit hitTower;
            transform.position = new Vector3(1000, 1000, 1000);
            if (Physics.Raycast(ray, out hitTower, 100))
            {
                transform.position = hitTower.point;
                 if(Input.GetMouseButtonDown(0) && _hitCounter == 0)
                {
                    GameObject towerInstance = Instantiate(item) as GameObject;
                    ConstructionController constructionController = towerInstance.GetComponent<ConstructionController>();
                    Destroy(constructionController);
                    towerInstance.transform.position = transform.position;
                    towerInstance.gameObject.renderer.material.color = Color.black;
                    Destroy(towerInstance.gameObject.collider);

                     //add box collider to tower
                    BoxCollider boxCollider = towerInstance.AddComponent<BoxCollider>();
                    boxCollider.size = new Vector3(RangeTower, RangeTower, boxCollider.size.z);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
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