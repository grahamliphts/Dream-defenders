using UnityEngine;
using System.Collections;

public class ModelTowerPoolManager : MonoBehaviour 
{
    [SerializeField]
    private TowerConstructionScript[] _modelTowers;

    public TowerConstructionScript GetFireTower()
    {
        var tower = _modelTowers[0];
        return tower;
    }

    public TowerConstructionScript GetElectricTower()
    {
        var tower = _modelTowers[1];
        return tower;
    }

    public void SetConstruction(bool value)
    {
        if (value == false)
        {
            for (int i = 0; i < _modelTowers.Length; i++)
            {
                if (_modelTowers[i].ConstructionController.GetConstruction() == true)
                {
                    _modelTowers[i].constructionController.SetConstruction(false);
                    _modelTowers[i].newtransform.position = new Vector3(1000, 1000, 1000);
                    _modelTowers[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
			_modelTowers[0].constructionController.Reset();
            _modelTowers[0].gameObject.SetActive(true);
			_modelTowers[0].constructionController.enabled = true;
			_modelTowers[0].constructionController.SetConstruction(true);
        }
            
    }
}
