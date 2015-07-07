using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tuto : MonoBehaviour 
{
	public Text tutoMessage;
	public TowerController towerController;
	public ConstructionController constructionControllerFire;
	public ConstructionController constructionControllerElec;


	//UI
	public Text GameInfo;
	public GameObject panelTower;
	public GameObject panelActions;
	public ModelTowerPoolManager modelTower;
	public GameObject playerInfos;
	public GameObject generalInfos;
	public SpellController spellController;
	private EndGame _endGame;
	private EnnemyManager _ennemyManager;

	bool _run = false;

	void Start()
	{
		_endGame = GetComponent<EndGame>();
	}

	void Update()
	{
		if (LevelStart.instance.modeTuto && _run == false)
		{
			_run = true;
			StartCoroutine("LoadTuto");
			_ennemyManager = GetComponent<EnnemyManager>();
		}
	}

	IEnumerator LoadTuto()
	{
		tutoMessage.text = "Bienvenue dans le tutoriel de DreamDefenders\n Appuyez sur entrée pour continuer";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Vous etes apparu a coté de votre nexus\n Vous devez le proteger des ennemis";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Pour vous déplacer utiliser les touches Z/S/Q/D \n Utilisez la touche left shift pour sprinter";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Vos informations générales sont en haut de l'écran";
		generalInfos.SetActive(true);
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Vos points de vie et de mana sont affichés en haut à gauche";
		playerInfos.SetActive(true);
		yield return StartCoroutine(WaitForKeyPress());

		/*Tower Construction*/
		tutoMessage.text = "Au début de chaque partie, vous commencez en mode construction";
		panelTower.SetActive(true);
		panelActions.SetActive(true);
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Des informations sur le nombre de tours restantes apparaissent à gauche";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Vous pouvez changer de type de tour en utilisant les touches de 1 à 4";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Posez une tour avec le clic gauche";
		yield return new WaitForSeconds(2);
		towerController.enabled = true;
		LoopManager.modeConstruction = true;
		tutoMessage.text = "";
		GameInfo.text = "Objectif : Poser une tour";
		yield return StartCoroutine(WaitForPlaceTower());
		GameInfo.text = "";
		tutoMessage.text = "Nous allons maintenant passer en mode combat";
		yield return StartCoroutine(WaitForKeyPress());
		LoopManager.modeConstruction = false;
		towerController.enabled = false;
		modelTower.SetConstruction(false);

		/*Ennemies*/
		tutoMessage.text = "Les ennemis peuvent etre de différents types : Feu, Electrique, Poison et Glace";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Vos sorts sont aussi de différents types\n Utilisez les touches de 1 à 4 pour changer de type";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Utilisez vos types de sorts judicieusement pour combattre les ennemis";
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Un enemi de feu vient d'apparaitre\n Essayez de vous en débarasser";
		_ennemyManager.Spawn();
		yield return StartCoroutine(WaitForKeyPress());
		tutoMessage.text = "Pour tirer utilisez le clic gauche de la souris";
		spellController.enabled = true;
		yield return new WaitForSeconds(1);
		tutoMessage.text = "";
		GameInfo.text = "Objectif : Tuer les ennemis";
		yield return StartCoroutine(WaitForEnnemyDie());
		tutoMessage.text = "Félicitations, vous avez tué tout les ennemis";
		yield return new WaitForSeconds(2);
		tutoMessage.enabled = false;
		_endGame.CloseGame("Fin du tuto");
	}

	IEnumerator WaitForEnnemyDie()
	{
		while (!_ennemyManager.AllDied())
			yield return null;
	}

	IEnumerator WaitForPlaceTower()
	{
		while ((!Input.GetMouseButtonDown(0)) || towerController.hitCounter != 0)
			yield return null;
	}

	IEnumerator WaitForKeyPress()
	{
		yield return new WaitForSeconds(0.2f);
		while (!Input.GetKeyDown(KeyCode.Return))
			yield return null;
	}

	IEnumerator WaitForChangeTower()
	{
		while (!Input.GetKey("3"))
			yield return null;
	}
}
