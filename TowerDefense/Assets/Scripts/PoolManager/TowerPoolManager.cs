using UnityEngine;
using System.Collections;

public class TowerPoolManager : MonoBehaviour 
{
    [SerializeField]
    public TowerScript[] _towers;
    int _index = 0;

    public TowerScript GetTower()
    {
		if (_index >= GetTowerNumberMax())
			return null;
		Debug.Log(_index);
        var tower = _towers[_index];
        _index++;
		Debug.Log(_index);
		return tower;
    }

    public int GetTowersNumber()
    {
        int number = 0;
        for (int i = 0; i < _towers.Length; i++)
        {
            if(_towers[i].gameObject.activeSelf == true)
                number++;
        }
        return number;
    }

    public int GetTowerNumberMax()
    {
        return _towers.Length;
    }
}
