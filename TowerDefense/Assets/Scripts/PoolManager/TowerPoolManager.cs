using UnityEngine;
using System.Collections;

public class TowerPoolManager : MonoBehaviour 
{
    [SerializeField]
    public TowerScript[] _towers;
    int _index = 0;

    public TowerScript GetTower()
    {
		if (_index >= _towers.Length)
		{
			Debug.Log("DEBUG: too much Tower availables in LevelStart Script");
			return null;
		}

        var tower = _towers[_index];
        _index++;
		return tower;
    }
}
