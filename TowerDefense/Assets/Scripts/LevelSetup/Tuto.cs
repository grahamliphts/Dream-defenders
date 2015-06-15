using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tuto : MonoBehaviour 
{
	public Text tutoMessage;

	void Start () 
	{
		tutoMessage.text = "Voulez vous activer le tutorial ? (0/N)";
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			StartCoroutine("LoadTuto");
			Debug.Log("StartCoroutine");
		}
			
		else if (Input.GetKeyDown(KeyCode.N))
		{
			tutoMessage.enabled = false;
		}
			
	}

	IEnumerator LoadTuto()
	{
		tutoMessage.text = "Bienvenue dans le tutoriel de DreamDefenders\n(Appuyez sur Entrée)";
		yield return StartCoroutine(WaitKeyDown(KeyCode.Return));
		tutoMessage.text = "Pour vous deplacer utiliser les touches Z/S/Q/D et la touche left shift pour sprinter";
		yield return StartCoroutine(WaitKeyDown(KeyCode.Return));
		tutoMessage.text = "Pour poser une tour utilisez le clic gauche de la souris \n (en mode construction) \n lorsque la tour devient verte";
		yield return StartCoroutine(WaitKeyDown(KeyCode.Return));
		tutoMessage.text = "Pour tirer utilisez le clic gauche de la souris \n (en mode combat)";
		yield return StartCoroutine(WaitKeyDown(KeyCode.Return));
		tutoMessage.text = "Vous pouvez changer de sorts ou de tours en utilisant les touches de 1 à 4";
		yield return StartCoroutine(WaitKeyDown(KeyCode.Return));
		tutoMessage.text = "Les ennemis possédent certaines faiblesses, adapter vos sorts en fonction des ennemis";
		yield return StartCoroutine(WaitKeyDown(KeyCode.Return));
		tutoMessage.enabled = false;
	}

	IEnumerator WaitKeyDown(KeyCode keyCode)
	{
		while (!Input.GetKeyDown(keyCode))
			yield return null;

	}
}
