using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour 
{
	public Text ConstructionTime;
	public GuiInGameManager guiInGame;

	//Infos du jeu
	public Text gameInfo;
	[SerializeField]
	private int _constructionTime;
	public int waveNumber;
	[SerializeField]
	private int _ennemyAddedByWaveMin;
	[SerializeField]
	private int _ennemyAddedByWaveMax;

    public ModelTowerPoolManager ModelTower;

    private EnnemyManager _ennemyManager;
	public EnnemyManager ennemyManager
	{
		get
		{
			return _ennemyManager;
		}
	}

    private int _actualWave;
	public int actualWave
	{
		get
		{
			return _actualWave;
		}
	}

    private float _startTime;
    public static bool modeConstruction;
    private bool _win;
	public bool win
	{
		set
		{
			_win = value;
		}
		get
		{
			return _win;
		}
	}
    private bool _lose;
	public bool lose
	{
		set
		{
			_lose = value;
		}
	}
	private bool _closingParty;
	private NetworkView _networkView;
	private bool _init;
	private bool _inter;
	private float _interTimer;

	void Start()
	{
		_init = false;
		_ennemyManager = GetComponent<EnnemyManager>();
		_networkView = GetComponent<NetworkView>();
		_inter = false;
	}

	public void Init() 
    {
		//A mettre ailleurs
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameInfo.text = "Poser des Tours";
        ModelTower.SetConstruction(true);
        _win = false;
        _actualWave = 0;
		_startTime = Time.time;
		modeConstruction = true;
		_init = true;
		_interTimer = 0.0f;
	}
	
	void Update () 
    {
		if ((!LevelStart.instance.modeMulti || Network.isServer) && _init == true)
		{
			if (_lose == false)
			{
				if (modeConstruction)
				{
					int constructionTime = _constructionTime - (int)(Time.time - _startTime) % 60;
					ConstructionTime.text = constructionTime.ToString();
				}

				//Temps de construction fini
				if (((int)(Time.time - _startTime) % 60) >= _constructionTime && modeConstruction && !_inter)
				{
					guiInGame.Reset();
					ConstructionTime.text = "None";
					if (LevelStart.instance.modeMulti)
						_networkView.RPC("SyncConstruction", RPCMode.All, false);
					else
						SetConstruction(false);

					Debug.Log("Begin inter Time");
					modeConstruction = false;
					_inter = true;
					_interTimer = Time.time;
				}

				if (_inter && (int)(Time.time - _interTimer) % 60 >= 3 && modeConstruction)
				{
					Debug.Log("Spawn Enemies");
					_ennemyManager.Spawn();
					_inter = false;
				}

				//Ennemis morts / wave actuelle < waves totale
				if (_ennemyManager.AllDied() == true && _actualWave < waveNumber && !modeConstruction)
				{
					modeConstruction = true;
					_inter = true;
					_interTimer = Time.time;
				}
				if (_inter && (int)(Time.time - _interTimer) % 60 >= 3 && !modeConstruction)
				{
					_actualWave++;
					//Ajout d'ennemis a spawn
					int nbMax = actualWave;
					_ennemyManager.AddEnemiesElec(Random.Range(0, nbMax));
					_ennemyManager.AddEnemiesFire(Random.Range(0, nbMax));
					_ennemyManager.AddEnemiesIce(Random.Range(0, nbMax));
					_ennemyManager.AddEnemiesPoison(Random.Range(0, nbMax));
					if (_actualWave > 3)
						nbMax = 2;

					if (LevelStart.instance.modeMulti)
						_networkView.RPC("SyncConstruction", RPCMode.All, true);
					else
						SetConstruction(true);

					_startTime = Time.time;
					_inter = false;
				}
			}
		}
    }

	[RPC]
	private void SyncConstruction(bool construction)
	{
		SetConstruction(construction);
	}

	public void SetConstruction(bool construction)
	{
		ModelTower.SetConstruction(construction);
		if (construction == false)
			gameInfo.text = "Wave " + (_actualWave + 1) + "/" + waveNumber;
		else
			gameInfo.text = "Poser des Tours";
	}
}
