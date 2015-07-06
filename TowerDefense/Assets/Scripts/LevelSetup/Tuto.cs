using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tuto : MonoBehaviour 
{
	public Text tutoMessage;
	public TowerController towerController;
	public ConstructionController constructionControllerFire;
	public ConstructionController constructionControllerElec;
	public Text GameInfo;
	public GameObject panelTower;
	public GameObject panelActions;
	public GameObject modelTower;
	public SpellController spellController;
	private EnnemyManager _ennemyManager;

	int _type;
	bool _run = false;
	void Update()
	{
		if (LevelStart.instance.modeTuto && _run == false)
		{
			_run = true;
			StartCoroutine("LoadTuto");
			_type = 0;
			_ennemyManager = GetComponent<EnnemyManager>();
		}
	}

	IEnumerator LoadTuto()
	{
		tutoMessage.text = "Bienvenue dans le tutoriel de DreamDefenders";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Pour vous deplacer utiliser les touches Z/S/Q/D \n Utilisez la touche left shift pour sprinter";
		yield return new WaitForSeconds(4);
		tutoMessage.text = "Vous etes apparu a coté de votre nexus\n Vous devez le proteger des ennemis";
		yield return new WaitForSeconds(4);

		/*Tower Construction*/
		tutoMessage.text = "Au début de chaque partie, vous commencez en mode construction";
		panelTower.SetActive(true);
		panelActions.SetActive(true);
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Des informations sur le nombre de tours restantes apparaissent à gauche";
		yield return new WaitForSeconds(2);
		towerController.Reset();
		LoopManager.modeConstruction = true;
		tutoMessage.text = "Posez une tour avec le clic gauche";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "";
		GameInfo.text = "Objectif : Poser une tour";
		yield return StartCoroutine(WaitForPlaceTower());
		GameInfo.text = "";
		tutoMessage.text = "Felicitations, vous avez poser une tour de feu";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Vous pouvez changer de types de tours en utilisant les touches de 1 à 4";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Appuyez sur la touche 3";
		yield return new WaitForSeconds(1);
		tutoMessage.text = "";
		GameInfo.text = "Objectif : Changer de tour";
		yield return StartCoroutine(WaitForChangeTower());
		tutoMessage.text = "Felicitations, vous pouvez maintenant poser une tour poison";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "";
		GameInfo.text = "Objectif : Poser une deuxieme tour";
		yield return StartCoroutine(WaitForPlaceTower());
		GameInfo.text = "";
		tutoMessage.text = "Nous allons maintenant passer en mode combat";
		yield return new WaitForSeconds(2);
		LoopManager.modeConstruction = false;
		modelTower.SetActive(false);

		/*Ennemies*/
		tutoMessage.text = "Les ennemis peuvent etre de différents types : Feu, Electrique, Poison et Glace";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Vos sorts sont aussi de différents types\n Utilisez les touches de 1 à 4 pour changer de type";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Utilisez vos types de sorts judicieusement pour combattre les ennemis";
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Un enemi de feu vient d'apparaitre\n Essayez de vous en débarasser";
		_ennemyManager.Spawn();
		yield return new WaitForSeconds(2);
		tutoMessage.text = "Pour tirer utilisez le clic gauche de la souris";
		spellController.enabled = true;
		yield return new WaitForSeconds(1);
		tutoMessage.text = "";
		GameInfo.text = "Objectif : Tuer les ennemis";
		yield return StartCoroutine(WaitForEnnemyDie());
		tutoMessage.text = "Félicitations, vous avez tué tout les ennemis";
		yield return new WaitForSeconds(1);
		tutoMessage.enabled = false;
	}

	IEnumerator WaitForEnnemyDie()
	{
		while (!_ennemyManager.AllDied())
			yield return null;
	}

	IEnumerator WaitForPlaceTower()
	{
		while (!Input.GetMouseButtonDown(0))
			yield return null;
		if(_type == 0)
			towerController.PlaceTower(constructionControllerFire.transform.position, _type);
		else
			towerController.PlaceTower(constructionControllerElec.transform.position, _type);
	}

	IEnumerator WaitForChangeTower()
	{
		while (!Input.GetKey("3"))
			yield return null;
		_type = 2;
		towerController.ChangeTower(_type);
	}
}
