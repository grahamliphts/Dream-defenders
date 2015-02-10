using UnityEngine;
using System.Collections;

public class GuiInGameManager : MonoBehaviour 
{
    public ModelTowerPoolManager ModelTowerPoolManager;
    private TowerConstructionScript _target;
    private TowerConstructionScript _newTarget;

	void Start () 
    {
        _target = ModelTowerPoolManager.GetFrozenTower();
        _target.gameObject.SetActive(true);
        _target.ConstructionController.enabled = true;
	}
	
	void Update () 
    {
        if (Input.GetKey("1"))
        {
            if (_target != ModelTowerPoolManager.GetFrozenTower())
            {
                _newTarget = ModelTowerPoolManager.GetFrozenTower();
                SetTower(_target, _newTarget);
                _target = _newTarget;
            }
        }
        if (Input.GetKey("2"))
        {
            if (_target != ModelTowerPoolManager.GetFireTower())
            {
                 _newTarget = ModelTowerPoolManager.GetFireTower();
                SetTower(_target, _newTarget);
                _target = _newTarget;
            }
        }
        if (Input.GetKey("3"))
        {
            if (_target != ModelTowerPoolManager.GetElectricTower())
            {
                _newTarget = ModelTowerPoolManager.GetElectricTower();
                SetTower(_target, _newTarget);
                _target = _newTarget;
            }
        }
	}

    void SetTower(TowerConstructionScript previousTower, TowerConstructionScript tower)
    {
        previousTower.ConstructionController.enabled = false;
        previousTower.Transform.position = new Vector3(1000, 1000, 1000);
        previousTower.gameObject.SetActive(false);

        tower.gameObject.SetActive(true);
        tower.ConstructionController.enabled = true;
        tower.ConstructionController.SetConstruction(true);
    }
}
