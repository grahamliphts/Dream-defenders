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
			return null;
        var tower = _towers[_index];
        _index++;
		return tower;
    }

	public int GetNbMax()
	{
		int nb = 0;
		for (int i = 0; i < _towers.Length; i++)
			nb++;
		return nb;
	}
}
