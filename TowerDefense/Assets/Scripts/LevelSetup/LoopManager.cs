﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour 
{
    public ModelTowerPoolManager ModelTower;
    public Text TimeInfo;
    public Text GameInfo;
    public Text ValueElectricTower;
    public Text ValueFireTower;
    public GameObject Player;
    public TowerPoolManager FirePool;
    public TowerPoolManager ElectricPool;

    [SerializeField]
    private int _waveNumber;
    [SerializeField]
    private int _ennemyAddedByWave;
    [SerializeField]
    private int _constructionTime;

    private EnnemyManager m_ennemyManager;
    private int _actualWave;
    private float _startTime;

    private bool _modeConstruction;

    private int _nbFireTower;
    private int _nbElectricTower;
    private bool _win;
    private bool _lose;

    private PlayerLifeManager _lifeManager;

	void Start () 
    {
        GameInfo.text = "Poser des Tours";
        ModelTower.SetConstruction(true);
        _modeConstruction = true;
        _nbFireTower = ElectricPool.GetTowerNumberMax();
        _nbElectricTower = FirePool.GetTowerNumberMax();
        _win = false;
        _startTime = Time.time;
        m_ennemyManager = GetComponent<EnnemyManager>();
        _actualWave = 0;
        _lifeManager = Player.GetComponent<PlayerLifeManager>();
	}
	
	void Update () 
    {
        if(_lose == false)
        {
            ValueElectricTower.text = (_nbElectricTower - ElectricPool.GetTowersNumber()).ToString();
            ValueFireTower.text = (_nbFireTower - FirePool.GetTowersNumber()).ToString();

            TimeInfo.text = ((int)(Time.time - _startTime) % 60).ToString();
            if (((int)(Time.time - _startTime) % 60) >= _constructionTime && _modeConstruction == true)
            {
                ModelTower.SetConstruction(false);
                _modeConstruction = false;
                GameInfo.text = "Wave" + _actualWave;
                m_ennemyManager.Spawn();
            }
            else if (m_ennemyManager.AllDied() == true && _actualWave < _waveNumber && _modeConstruction == false)
            {
                _actualWave++;
                m_ennemyManager.AddEnemies(_ennemyAddedByWave);
                GameInfo.text = "Poser des Tours";
                ModelTower.SetConstruction(true);
                _modeConstruction = true;
                _startTime = Time.time;
            }
            else if (_actualWave == _waveNumber && m_ennemyManager.AllDied() == true && _win == false)
            {
                GameInfo.text = "You Win";
                _win = true;
            }

            if (_lifeManager.GetLife() <= 0)
            {
                GameInfo.text = "You died";
                _lose = true;
            }
        }
    }
}
