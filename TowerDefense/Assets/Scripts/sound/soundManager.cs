using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	public static SoundManager instance = null;
	public AudioSource _source;
	

	public AudioClip _tpCrossing;
	public AudioClip _spawn;

	void Start () 
	{
		instance = this;
	}

	public void PlayTpCrossing()
	{
		_source.clip = _tpCrossing;
		_source.Play();	
	}
	public void PlaySpawn()
	{
		_source.clip = _spawn;
		_source.Play();
	}

}
