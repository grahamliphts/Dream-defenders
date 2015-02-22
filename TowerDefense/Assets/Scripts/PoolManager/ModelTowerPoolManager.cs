using UnityEngine;
using System.Collections;

public class ModelTowerPoolManager : MonoBehaviour 
{
    public TowerConstructionScript[] ModelTowers;
    public TowerConstructionScript GetFireTower()
    {
        var tower = ModelTowers[0];
        return tower;
    }

    public TowerConstructionScript GetElectricTower()
    {
        var tower = ModelTowers[1];
        return tower;
    }

    public void SetConstruction(bool value)
    {
        if (value == false)
        {
            for (int i = 0; i < ModelTowers.Length; i++)
            {

                if (ModelTowers[i].ConstructionController.GetConstruction() == true)
                {
                    ModelTowers[i].ConstructionController.SetConstruction(false);
                    ModelTowers[i].Transform.position = new Vector3(1000, 1000, 1000);
                    ModelTowers[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            ModelTowers[0].ConstructionController.Reset();
            ModelTowers[0].gameObject.SetActive(true);
            ModelTowers[0].ConstructionController.enabled = true;
            ModelTowers[0].ConstructionController.SetConstruction(true);
        }
            
    }
}
