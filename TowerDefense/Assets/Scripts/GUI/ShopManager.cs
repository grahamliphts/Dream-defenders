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
		spirit.text = "Esprit : " + _stats.esprit;
		regen.text = "Regen : " + _stats.regen;
		upSpirit.text = spiritCost + "$";

		//endurance
		endurance.text = "Endurance : " + _stats.endurance;
		life.text = "Vie : " + _stats.lifeMax;
		upEndu.text = enduCost + "$";

		//robustesse
		robustesse.text = "Robustesse : " + _stats.robustesse;
		upRobu.text = robuCost + "$";

		//intelligence
		intelligence.text = "Robustesse : " + _stats.intelligence;
		upIntel.text = intelCost + "$";

		//money 
		money.text = levelManager.money.ToString();
	}

	void refresh(uint toRefresh)
	{
		//spirit
		if (toRefresh == 0) 
		{
			spirit.text = "Esprit : " + _stats.esprit;
			regen.text = "Regen : " + _stats.regen;
			upSpirit.text = spiritCost + "$";
		} 
		//endurance
		else if (toRefresh == 1) 
		{
			endurance.text = "Endurance : " + _stats.endurance;
			life.text = "Vie : " + _stats.lifeMax;
			upEndu.text = enduCost + "$";
		} 
		//robustesse
		else if (toRefresh == 2)
		{
			robustesse.text = "Robustesse : " + _stats.robustesse;
			damageReduc.text = "Reduction dommages : " + _stats.damageReduction;
			upRobu.text = robuCost + "$";
		}
		//intelligence
		else if (toRefresh == 3)
		{
			intelligence.text = "Intelligence : " + _stats.intelligence;
			degats.text = "Degats en plus : " + _stats.degatsAdd;
			upIntel.text = intelCost + "$";
		}
		money.text = levelManager.money.ToString();
	}

	public void UpSpirit(int amount)
	{
		if (spiritCost <= levelManager.money)
		{
			levelManager.money -= spiritCost;
			_stats.esprit += 1;
			spiritCost = (int)(spiritCost * costIncrement);
			refresh(0);
		}
	}

	public void UpEndu(int amount)
	{
		levelManager.money -= enduCost;
		if (enduCost <= levelManager.money)
		{
			_stats.endurance += 1;
			enduCost = (int)(enduCost * costIncrement);
			refresh(1);
		}
	}

	public void UpRobu(int amount)
	{
		levelManager.money -= robuCost;
		if (robuCost <= levelManager.money)
		{
			_stats.robustesse += 1;
			robuCost = (int)(robuCost * costIncrement);
			refresh(2);
		}
	}

	public void UpIntel(int amount)
	{
		levelManager.money -= intelCost;
		if (intelCost <= levelManager.money)
		{
			_stats.intelligence += 1;
			intelCost = (int)(intelCost * costIncrement);
			refresh(3);
		}
	}
}
