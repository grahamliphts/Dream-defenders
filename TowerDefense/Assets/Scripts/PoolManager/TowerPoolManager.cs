using UnityEngine;
using System.Collections;

public class TowerPoolManager : MonoBehaviour 
{

    public TowerScript[] Towers;
    int _index = 0;
    public TowerScript GetTower()
    {
        var tower = Towers[_index];
        _index++;
        return tower;
    }
}
