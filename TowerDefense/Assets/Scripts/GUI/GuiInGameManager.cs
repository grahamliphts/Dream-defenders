using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiInGameManager : MonoBehaviour 
{
    public ModelTowerPoolManager ModelTowerPoolManager;
    private TowerConstructionScript _target;
    private TowerConstructionScript _newTarget;

	public Shadow action1;
	public Shadow action2;
	public Shadow action3;
	public Shadow action4;

	private Shadow _shadowTarget;

	void Start () 
    {
        _target = ModelTowerPoolManager.GetFireTower();
        _target.gameObject.SetActive(true);
        _target.ConstructionController.enabled = true;

		_shadowTarget = action1;
	}
	
	void Update () 
    {
		if (_target.ConstructionController.enabled == true)
		{
			if (Input.GetKey("1"))
			{
				if (_target != ModelTowerPoolManager.GetFireTower())
				{
					_newTarget = ModelTowerPoolManager.GetFireTower();
					SetTower(_target, _newTarget);
					_target = _newTarget;
				}
			}
			if (Input.GetKey("2"))
			{

				action2.effectColor = Color.yellow;
				if (_target != ModelTowerPoolManager.GetElectricTower())
				{
					_newTarget = ModelTowerPoolManager.GetElectricTower();
					SetTower(_target, _newTarget);
					_target = _newTarget;
				}
			}
		}

		if (Input.GetKey("1"))
		{
			SetColor(_shadowTarget, action1);
		}
		if (Input.GetKey("2"))
		{
			SetColor(_shadowTarget, action2);
		}

       else if (Input.GetKey("3"))
        {
			SetColor(_shadowTarget, action3);
        }

	   else if (Input.GetKey("4"))
		{
			SetColor(_shadowTarget, action4);
		}
	}

	void SetColor(Shadow previous, Shadow current)
	{
		previous.effectColor = Color.black;
		current.effectColor = Color.yellow;

		_shadowTarget = current;
	}

    void SetTower(TowerConstructionScript previousTower, TowerConstructionScript tower)
    {
        previousTower.gameObject.SetActive(false);
        previousTower.ConstructionController.enabled = false;
        previousTower.Transform.position = new Vector3(1000, 1000, 1000);
        

        tower.gameObject.SetActive(true);
        tower.ConstructionController.enabled = true;
        tower.ConstructionController.SetConstruction(true);
    }

	public void SetConstructionController(bool construction)
	{
		_target.ConstructionController.enabled = construction;
	}
}
