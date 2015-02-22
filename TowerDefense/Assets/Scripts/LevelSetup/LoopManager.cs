using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour 
{
    public ModelTowerPoolManager ModelTower;
    public Text TimeInfo;
    public Text GameInfo;

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
    private bool _modeBattle;

	void Start () 
    {
        GameInfo.text = "Poser des Tours";
        ModelTower.SetConstruction(true);
        _modeConstruction = true;
        _modeBattle = false;
        _startTime = Time.time;
        m_ennemyManager = GetComponent<EnnemyManager>();
        _actualWave = 1;
	}
	
	void Update () 
    {

        TimeInfo.text = ((int)(Time.time - _startTime) % 60).ToString();
        if (((int)(Time.time - _startTime) % 60) >= _constructionTime && _modeConstruction == true)
        {
            ModelTower.SetConstruction(false);
            _modeConstruction = false;
            _modeBattle = true;
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
        else if(_actualWave == _waveNumber && m_ennemyManager.AllDied() == true)
            GameInfo.text = "You Win";
    }
}
