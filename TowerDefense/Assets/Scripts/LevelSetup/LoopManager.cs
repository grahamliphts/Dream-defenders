using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour 
{
	//Pour la fermeture de la partie
	public RawImage ImageClose;
	public Text CloseInfo;
	public Text Tuto;
	//Infos du jeu
	public Text GameInfo;

	[SerializeField]
	private int _constructionTime;
	[SerializeField]
	private int _waveNumber;
	[SerializeField]
	private int _ennemyAddedByWave;

    public ModelTowerPoolManager ModelTower;

    private EnnemyManager _ennemyManager;
    private int _actualWave;
    private float _startTime;

	public GameObject Player;
    public static bool modeConstruction;

    private bool _win;
    private bool _lose;

	//Verification de la vie
    private PlayerLifeManager _lifeManager;
	public NexusLife LifeNexus;
    private float _timer;

	private bool _closingParty;

	void Start()
	{
		_startTime = Time.time;
		_ennemyManager = GetComponent<EnnemyManager>();
	}

	public void Init(PlayerLifeManager lifeManager) 
    {
		//A mettre ailleurs
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameInfo.text = "Poser des Tours";
        ModelTower.SetConstruction(true);
		modeConstruction = true;

        _win = false;
        
        _actualWave = 0;
		_lifeManager = lifeManager;

        _timer = 0;
	}
	
	void Update () 
    {
		if (_lifeManager == null)
			return;
        if(_lose == false)
        {			
			//Temps de construction fini
			if (((int)(Time.time - _startTime) % 60) >= _constructionTime && modeConstruction == true)
            {
				ModelTower.SetConstruction(false);
				modeConstruction = false;
                GameInfo.text = "Wave " + (_actualWave + 1);
				_ennemyManager.Spawn();
            }

			//Ennemis morts / wave actuelle < waves totale
			else if (_ennemyManager.AllDied() == true && _actualWave < _waveNumber && modeConstruction == false)
            {
                _actualWave++;
				//Ajout d'ennemis a spawn
				_ennemyManager.AddEnemiesElec(_ennemyAddedByWave);
				_ennemyManager.AddEnemiesFire(_ennemyAddedByWave);
				_ennemyManager.AddEnemiesIce(_ennemyAddedByWave);
				_ennemyManager.AddEnemiesPoison(_ennemyAddedByWave);

				//Set mode construction
                GameInfo.text = "Poser des Tours";
				ModelTower.SetConstruction(true);
				modeConstruction = true;
                _startTime = Time.time;
            }

			if(Network.isServer)
			{
				if (_actualWave == _waveNumber && _ennemyManager.AllDied() == true && _win == false)
				{
					CloseParty("You Win");
					_win = true;
				}
			}
			
        }

        if (_lifeManager.GetLife() <= 0)
			CloseParty("You died");

		else if (LifeNexus.GetLife() <= 0)
			CloseParty("Nexus has been destroyed");
    }

	private void CloseParty(string text)
	{
		_closingParty = true;

		GameInfo.text = "";
		Tuto.text = "";

		ImageClose.gameObject.SetActive(true);
		CloseInfo.text = text;
		
		_lose = true;
		_timer += Time.deltaTime;
		if (_timer >= 3)
		{
			Network.Disconnect();
			Application.LoadLevel("MenuScene");
		}
	}

	public bool GetClosingParty()
	{
		return _closingParty;
	}

	private void OnDisconnectedFromServer(NetworkDisconnection msg)
	{
		Debug.Log(msg);
		CloseParty("Disconnect");
	}
}
