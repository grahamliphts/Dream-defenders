using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	public static SoundManager instance = null;
	public AudioSource source;
	

	public AudioClip _tpCrossing;
	public AudioClip _spawn;

	void Start () 
	{
		instance = this;
	}

	public void PlayTpCrossing()
	{
		source.clip = _tpCrossing;
		source.Play();	
	}
	public void PlaySpawn()
	{
		source.clip = _spawn;
		source.Play();
	}

}
