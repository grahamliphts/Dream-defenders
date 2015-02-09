using UnityEngine;
using System.Collections;

public class TowerPoolManager : MonoBehaviour 
{

    public FrozenTowerScript[] FrozenTowers;
    int _index = 0;
    public FrozenTowerScript GetFrozenTower()
    {
        var tower = FrozenTowers[_index];
        _index++;
        return tower;
    }
}
