using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	public static SoundManager instance = null;
	public AudioSource _source;
	
	public AudioClip _startCall;
	public AudioClip _endCall;
	public AudioClip _tpCrossing;
	public AudioClip _bearDie;
	public AudioClip _nexusDie;

	void Start () 
	{
		instance = this;
	}

	public void PlayStartCall(){
		_source.clip = _startCall;
		_source.Play();	
	}
	
	public void PlayEndCall(){
		_source.clip = _endCall;
		_source.Play();	
	}
	
	public void PlayTpCrossing(){
		_source.clip = _tpCrossing;
		_source.Play();	
	}
	
	public void PlayBearDie(){
		_source.clip = _bearDie;
		_source.Play();	
	}
	
	public void PlayNexusDie(){
		_source.clip = _nexusDie;
		_source.Play();	
	}
}
