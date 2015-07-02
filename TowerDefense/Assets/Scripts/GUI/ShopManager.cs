using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	[SerializeField] Stats m_playerData;

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

	// Use this for initialization
	void Start () {

			spirit.text = "Spirit : " + m_playerData.esprit;
			Regen.text = "Regen : " + m_playerData.regen;
			_Add_Spirit.text = "Upgrade $" + SpiritCost;
			endu.text = "Endurance : " + m_playerData.endurance;
			Life.text = "Life : " + m_playerData.lifeMax;
			_Add_Endu.text = "Upgrade $" + EnduCost;
			Robustesse.text = "Robustesse : " + m_playerData.robustesse;
			_Add_Robu.text = "Upgrade $" + RobuCost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void refresh(uint toRefresh)
	{
		if (toRefresh == 0) {
			spirit.text = "Spirit : " + m_playerData.esprit;
			Regen.text = "Regen : " + m_playerData.regen;
			_Add_Spirit.text = "Upgrade $" + SpiritCost;
		} else if (toRefresh == 1) {
			endu.text = "Endurance : " + m_playerData.endurance;
			Life.text = "Life : " + m_playerData.lifeMax;
			_Add_Endu.text = "Upgrade $" + EnduCost;
		} else if (toRefresh == 2) {
			Robustesse.text = "Robustesse : " + m_playerData.robustesse;
			_Add_Robu.text = "Upgrade $" + RobuCost;
		}

	}

	public void Add_Spirit(int amount)
	{
		Debug.Log ("Add_Spirit ");
		m_playerData.esprit += 1;
		SpiritCost = (int)(SpiritCost * CostIncrement);
		refresh (0);

	}

	public void Add_Endu(int amount)
	{
		Debug.Log ("Add_Endu ");
		m_playerData.endurance += 1;
		EnduCost = (int)(EnduCost * CostIncrement);
		refresh (1);
	}

	public void Add_Robu(int amount)
	{
		Debug.Log ("Add_Endu ");
		m_playerData.robustesse += 1;
		RobuCost = (int)(RobuCost * CostIncrement);
		refresh (2);
	}
}
