using UnityEngine;
using System.Collections;

public class ModelTowerPoolManager : MonoBehaviour 
{
    public TowerConstructionScript[] ModelTowers;

    public TowerConstructionScript GetFrozenTower()
    {
        var tower = ModelTowers[0];
        return tower;
    }

    public TowerConstructionScript GetFireTower()
    {
        var tower = ModelTowers[1];
        return tower;
    }

    public TowerConstructionScript GetElectricTower()
    {
        var tower = ModelTowers[2];
        return tower;
    }
}
