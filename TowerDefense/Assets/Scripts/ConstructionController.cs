using UnityEngine;
using System.Collections;

public class ConstructionController : MonoBehaviour
{
    public Object item;
    private uint _hitCounter;
    private float _distance = 3.0f;
    private bool _construction = false;
    // Update is called once per frame

    void Start()
    {
        _hitCounter = 0;
    }

    void Update()
    {
        if (Input.GetKey("t"))
        {
            _construction = true;
        }

        if (_construction == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
            RaycastHit hitTower;
            transform.position = new Vector3(1000, 1000, 100);
            if (Physics.Raycast(ray, out hitTower, 100))
            {
                transform.position = hitTower.point;
                 if(Input.GetMouseButtonDown(0) && _hitCounter == 0)
                {
                    GameObject towerInstance = Instantiate(item) as GameObject;
                    towerInstance.transform.position = transform.position;
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
            Debug.Log(other.tag);
        }

        Debug.Log(_hitCounter);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "ground")
        {
             _hitCounter--;
            if(_hitCounter == 0)
                transform.gameObject.renderer.material.color = Color.green;
           
        }
        Debug.Log("trigger exit" + _hitCounter);
    }
}