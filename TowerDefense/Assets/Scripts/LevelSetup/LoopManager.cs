using UnityEngine;
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
	public GuiInGameManager GuiManager;

    [SerializeField]
    private int _waveNumber;
    [SerializeField]
    private int _ennemyAddedByWave;
    [SerializeField]
    private int _constructionTime;

    private EnnemyManager _ennemyManager;
    private int _actualWave;
    private float _startTime;

    public static bool modeConstruction;

    private int _nbFireTower;
    private int _nbElectricTower;
    private bool _win;
    private bool _lose;

    private PlayerLifeManager _lifeManager;
    private float _timer;

	void Start()
	{
		_startTime = Time.time;
		_ennemyManager = GetComponent<EnnemyManager>();
	}
	public void Init(PlayerLifeManager lifeManager) 
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        GameInfo.text = "Poser des Tours";
        ModelTower.SetConstruction(true);
		modeConstruction = true;
        _nbFireTower = ElectricPool.GetTowerNumberMax();
        _nbElectricTower = FirePool.GetTowerNumberMax();
        _win = false;
        
        _actualWave = 0;
		_lifeManager = lifeManager;
		//Debug.Log(_lifeManager);

        _timer = 0;
	}
	
	void Update () 
    {
		if (_lifeManager == null)
			return;
        if(_lose == false)
        {
            ValueElectricTower.text = (_nbElectricTower - ElectricPool.GetTowersNumber()).ToString();
            ValueFireTower.text = (_nbFireTower - FirePool.GetTowersNumber()).ToString();
			
            TimeInfo.text = ((int)(Time.time - _startTime) % 60).ToString();
			if (((int)(Time.time - _startTime) % 60) >= _constructionTime && modeConstruction == true)
            {
				ModelTower.SetConstruction(false);
				modeConstruction = false;
				int wave = _actualWave + 1;
                GameInfo.text = "Wave " + wave;
				_ennemyManager.Spawn();
            }
			else if (_ennemyManager.AllDied() == true && _actualWave < _waveNumber && modeConstruction == false)
            {
                _actualWave++;
				_ennemyManager.AddEnemies(_ennemyAddedByWave);
				
                GameInfo.text = "Poser des Tours";
				ModelTower.SetConstruction(true);
				modeConstruction = true;
                _startTime = Time.time;
            }
			else if (_actualWave == _waveNumber && _ennemyManager.AllDied() == true && _win == false)
            {
                GameInfo.text = "You Win";
                _win = true;
            }
        }

        if (_lifeManager.GetLife() <= 0)
        {
            GameInfo.text = "You died";
            _lose = true;
            _timer += Time.deltaTime;
            Debug.Log("Timer" + _timer);
            if (_timer >= 3)
            {
                Debug.Log("Timer = 4");
                Application.LoadLevel("MenuScene");
            }
        }
    }
}
