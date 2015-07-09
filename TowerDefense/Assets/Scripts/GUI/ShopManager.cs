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

	[SerializeField] Text spirit;
	[SerializeField] Text endu;
	[SerializeField] Text Robustesse;

	[SerializeField] Text Regen;
	[SerializeField] Text Life;
	[SerializeField] Text DamageReduc;

	[SerializeField] Text _Add_Spirit;
	[SerializeField] Text _Add_Endu;
	[SerializeField] Text _Add_Robu;

	[SerializeField] public float CostIncrement = 1.15f;

	public int SpiritCost = 100;
	public int EnduCost = 100;
	public int RobuCost = 100;

	public void SetStatsShop()
	{
		spirit.text = "Esprit : " + _stats.esprit;
		Regen.text = "Regen : " + _stats.regen;
		_Add_Spirit.text = "Améliorer : " + SpiritCost + "$";
		endu.text = "Endurance : " + _stats.endurance;
		Life.text = "Vie : " + _stats.lifeMax;
		_Add_Endu.text = "Améliorer : " + EnduCost + "$";
		Robustesse.text = "Robustesse : " + _stats.robustesse;
		_Add_Robu.text = "Améliorer : " + RobuCost + "$";
	}

	void refresh(uint toRefresh)
	{
		if (toRefresh == 0) 
		{
			spirit.text = "Esprit : " + _stats.esprit;
			Regen.text = "Regen : " + _stats.regen;
			_Add_Spirit.text = "Améliorer : " + SpiritCost + "$";
		} else if (toRefresh == 1) {
			endu.text = "Endurance : " + _stats.endurance;
			Life.text = "Vie : " + _stats.lifeMax;
			_Add_Endu.text = "Améliorer : " + EnduCost + "$";
		} else if (toRefresh == 2) {
			Robustesse.text = "Robustesse : " + _stats.robustesse;
			_Add_Robu.text = "Améliorer : " + RobuCost + "$";
		}

	}

	public void Add_Spirit(int amount)
	{
		_stats.esprit += 1;
		SpiritCost = (int)(SpiritCost * CostIncrement);
		refresh (0);

	}

	public void Add_Endu(int amount)
	{
		_stats.endurance += 1;
		EnduCost = (int)(EnduCost * CostIncrement);
		refresh (1);
	}

	public void Add_Robu(int amount)
	{
		_stats.robustesse += 1;
		RobuCost = (int)(RobuCost * CostIncrement);
		refresh (2);
	}
}
