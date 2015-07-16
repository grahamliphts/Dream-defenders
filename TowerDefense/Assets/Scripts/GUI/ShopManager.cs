using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	[SerializeField] 
	private Stats _stats;
	public Stats stats
	{
		get
		{
			return _stats;
		}
		set
		{
			_stats = value;
		}
	}

	[SerializeField] 
	private Text spirit;
	[SerializeField]
	private Text endurance;
	[SerializeField]
	private Text robustesse;
	[SerializeField]
	private Text intelligence;

	[SerializeField]
	private Text regen;
	[SerializeField]
	private Text life;
	[SerializeField]
	private Text damageReduc;
	[SerializeField]
	private Text degats;

	[SerializeField] Text upSpirit;
	[SerializeField] Text upEndu;
	[SerializeField] Text upRobu;
	[SerializeField] Text upIntel;
	[SerializeField] Text money;

	private float costIncrement;

	private int spiritCost;
	private int enduCost;
	private int robuCost;
	private int intelCost;

	private LevelManager levelManager;

	public void Start()
	{
		spiritCost = 100;
		enduCost = 100;
		robuCost = 100;
		intelCost = 100;
		costIncrement = 1.15f;

		levelManager = GetComponent<LevelManager>();
	}

	public void SetStatsShop()
	{
		//esprit
		spirit.text = _stats.esprit.ToString();
		regen.text = _stats.regen.ToString();
		upSpirit.text = spiritCost + "$";

		//endurance
		endurance.text = _stats.endurance.ToString();
		life.text = _stats.lifeMax.ToString();
		upEndu.text = enduCost + "$";

		//robustesse
		robustesse.text = _stats.robustesse.ToString();
		damageReduc.text = _stats.damageReduction.ToString();
		upRobu.text = robuCost + "$";

		//intelligence
		intelligence.text = _stats.intelligence.ToString();
		degats.text = _stats.degatsAdd.ToString();
		upIntel.text = intelCost + "$";

		//money 
		money.text = levelManager.money.ToString();
	}

	void refresh(uint toRefresh)
	{
		//spirit
		if (toRefresh == 0) 
		{
			spirit.text = _stats.esprit.ToString();
			regen.text = _stats.regen.ToString();
			upSpirit.text = spiritCost + "$";
		} 
		//endurance
		else if (toRefresh == 1) 
		{
			endurance.text = _stats.endurance.ToString();
			life.text = _stats.lifeMax.ToString();
			upEndu.text = enduCost + "$";
		} 
		//robustesse
		else if (toRefresh == 2)
		{
			robustesse.text = _stats.robustesse.ToString();
			damageReduc.text = _stats.damageReduction.ToString();
			upRobu.text = robuCost + "$";
		}
		//intelligence
		else if (toRefresh == 3)
		{
			intelligence.text = _stats.intelligence.ToString();
			degats.text = _stats.degatsAdd.ToString();
			upIntel.text = intelCost + "$";
		}
		money.text = levelManager.money.ToString();
	}

	public void UpSpirit(int amount)
	{
		if (spiritCost <= levelManager.money)
		{
			_stats.esprit += 1;
			levelManager.money -= spiritCost;
			spiritCost = (int)(spiritCost * costIncrement);
			refresh(0);
		}
	}

	public void UpEndu(int amount)
	{
		if (enduCost <= levelManager.money)
		{
			_stats.endurance += 1;
			levelManager.money -= enduCost;
			enduCost = (int)(enduCost * costIncrement);
			refresh(1);
		}
	}

	public void UpRobu(int amount)
	{
		if (robuCost <= levelManager.money)
		{
			_stats.robustesse += 1;
			levelManager.money -= robuCost;
			robuCost = (int)(robuCost * costIncrement);
			refresh(2);
		}
	}

	public void UpIntel(int amount)
	{
		if (intelCost <= levelManager.money)
		{
			_stats.intelligence += 1;
			levelManager.money -= intelCost;
			intelCost = (int)(intelCost * costIncrement);
			refresh(3);
		}
	}
}
