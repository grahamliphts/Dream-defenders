using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour 
{
	//Mode
	public static bool modeConstruction;

	//GUI Feedback
	public Text construction;
	public Text feedBack;
	public Text inter;
	//GUI
	public GuiInGameManager guiInGame;

	//Infos du jeu
	[SerializeField]
	private int _constructionTime;
	public int waveNumber;
	[SerializeField]
	private int _ennemyAddedByWaveMin;
	[SerializeField]
	private int _ennemyAddedByWaveMax;

    public ModelTowerPoolManager ModelTower;

	//Ennemies Manage
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

	//EndGame
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

	//Time phase
	private float _startTime;
	private bool _init;
	private bool _inter;
	private float _interTimer;
	private bool _continue;

	private NetworkView _networkView;

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

		StartCoroutine("PopupMessage", "Posez des tours");
        ModelTower.SetConstruction(true);
        _win = false;
        _actualWave = 0;
		_startTime = Time.time;
		modeConstruction = true;
		_init = true;
		_interTimer = 0.0f;
		_continue = false;

		inter.text = "None";
		construction.text = "None";
	}
	
	void Update () 
    {
		if ((!LevelStart.instance.modeMulti || Network.isServer) && _init == true)
		{
			if (_lose == false)
			{
				//GUI
				if (modeConstruction && !_inter)
				{
					int constructionTime = _constructionTime - (int)(Time.time - _startTime) % 60;
					construction.text = constructionTime.ToString() + "/" + _constructionTime;
				}
				else if(_inter)
				{
					int interTime = 3 - (int)(Time.time - _interTimer) % 60;
					inter.text = interTime.ToString() + "/" + 3;
				}
				if (_actualWave % 3 == 0)
					_constructionTime = 45;
				else
					_constructionTime = 30;
				//Temps de construction fini
				if (((int)(Time.time - _startTime) % 60) >= _constructionTime && modeConstruction && !_inter)
				{
					StartCoroutine("PopupMessage", "Temps mort");
					construction.text = "None";
					if (LevelStart.instance.modeMulti)
						_networkView.RPC("SyncConstruction", RPCMode.All, false);
					else
						SetConstruction(false);
					modeConstruction = false;
					_inter = true;
					_interTimer = Time.time;
				}

				if (_inter && (int)(Time.time - _interTimer) % 60 >= 3 && !_continue)
				{
					StartCoroutine("PopupMessage", "Tuez les ennemis");
					_ennemyManager.Spawn();
					_inter = false;
					inter.text = "None";
				}

				//Ennemis morts / wave actuelle < waves totale
				if (_ennemyManager.AllDied() == true && _actualWave < waveNumber && !modeConstruction && !_inter)
				{
					StartCoroutine("PopupMessage", "Temps mort");
					_inter = true;
					_interTimer = Time.time;
					_continue = true;
				}

				if (_inter && (int)(Time.time - _interTimer) % 60 >= 3 && _continue)
				{
					_continue = false;
					StartCoroutine("PopupMessage", "Posez des tours");
					_actualWave++;
					//Ajout d'ennemis a spawn
					int nbMax = actualWave;
					_ennemyManager.AddEnemiesElec(Random.Range(0, nbMax));
					_ennemyManager.AddEnemiesFire(Random.Range(0, nbMax));
					_ennemyManager.AddEnemiesIce(Random.Range(0, nbMax));
					_ennemyManager.AddEnemiesPoison(Random.Range(0, nbMax));
					if (_actualWave > 3)
						nbMax = 2;

					modeConstruction = true;
					if (LevelStart.instance.modeMulti)
						_networkView.RPC("SyncConstruction", RPCMode.All, true);
					else
						SetConstruction(true);

					_startTime = Time.time;
					_inter = false;
					inter.text = "None";
				}
			}
		}
    }

	IEnumerator PopupMessage(string text)
	{
		feedBack.text = text;
		yield return new WaitForSeconds(2);
		feedBack.text = "";
	}


	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		bool modeConstructionS = modeConstruction;
		if (stream.isWriting)
		{
			modeConstructionS = modeConstruction;
			stream.Serialize(ref modeConstructionS);
		}
		else
		{
			stream.Serialize(ref modeConstructionS);
			modeConstruction = modeConstructionS;
		}
	}

	[RPC]
	private void SyncConstruction(bool construction)
	{
		SetConstruction(construction);
	}

	public void SetConstruction(bool construction)
	{
		guiInGame.Reset();
		ModelTower.SetConstruction(construction);
	}
}
