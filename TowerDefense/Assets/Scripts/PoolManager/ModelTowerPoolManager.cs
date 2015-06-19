using UnityEngine;
using System.Collections;

public class ModelTowerPoolManager : MonoBehaviour 
{
    [SerializeField]
    private TowerConstructionScript[] _modelTowers;

    public TowerConstructionScript GetTower(int index)
    {
        var tower = _modelTowers[index];
        return tower;
    }

    public void SetConstruction(bool value)
    {
        if (value == false)
        {
            for (int i = 0; i < _modelTowers.Length; i++)
            {
					_modelTowers[i].constructionController.enabled = false;
                    _modelTowers[i].newtransform.position = new Vector3(1000, 1000, 1000);
                    _modelTowers[i].gameObject.SetActive(false);
            }
        }
        else
        {
			_modelTowers[0].constructionController.Reset();
            _modelTowers[0].gameObject.SetActive(true);
			_modelTowers[0].constructionController.enabled = true;
        }
            
    }
}
