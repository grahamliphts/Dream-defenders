using UnityEngine;
using System.Collections;

public class GuiInGameManager : MonoBehaviour 
{
    public GameObject FireTower;
    public GameObject FrozenTower;
    public GameObject NeutralTower;
    public GameObject ElectricTower;

    private GameObject target;
	void Start () 
    {
        target = NeutralTower;
	}
	
	void Update () 
    {
        if (Input.GetKey("1"))
        {
            if (target != NeutralTower)
            {
                SetTower(target, NeutralTower);
                target = NeutralTower;
            }
        }
        if (Input.GetKey("2"))
        {
            if (target != FrozenTower)
            {
                SetTower(target, FrozenTower);
                target = FrozenTower;
            }
        }
        if (Input.GetKey("3"))
        {
            if (target != ElectricTower)
            {
                SetTower(target, ElectricTower);
                target = ElectricTower;
            }
        }
        if (Input.GetKey("4"))
        {
            if (target != FireTower)
            {
                SetTower(target, FireTower);
                target = FireTower;
            }
        }
       /* if (Input.GetKey("5"))
            SetTower(target, FireTower);
        if (Input.GetKey("6"))
            SetTower(target, FireTower);*/
	}

    void SetTower(GameObject previousTower, GameObject tower)
    {
        ConstructionController constructionController;
        constructionController = previousTower.GetComponent<ConstructionController>();
        Destroy(constructionController);
        previousTower.transform.position = new Vector3(1000, 1000, 1000);
        
        constructionController = tower.AddComponent<ConstructionController>();
        constructionController.item = tower;
        constructionController.SetConstruction(true);
    }
}
