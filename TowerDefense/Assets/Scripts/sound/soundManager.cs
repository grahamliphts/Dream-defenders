using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

	public AudioSource _source;
	
	public AudioClip _startCall;
	public AudioClip _endCall;
	public AudioClip _tpCrossing;
	public AudioClip _bearDie;
	public AudioClip _nexusDie;
	/*[SerializeField]
	LoopManager m_LoopManager;*/
	
	void Start () {
		//_source = GetComponents<AudioSource>();
		//Debug.Log("tableau de sons 1 =  "+_source.Length);
		/*_startCall = _source[0];
		_endCall = _source[1];*/
		//Debug.Log("tableau de sons 2 = "+_source.Length);
	}
	

	void Update () {
		/*Debug.Log("loopmanagerrrrrrrrrrrrrrr  = "+m_LoopManager.init);
		if(result == true){
			//_endCall.Play();
			_source.clip = _startCall;
			_source.Play();		
		}
		if(m_LoopManager.lose == true){
			//_endCall.PlayOneShot;
			_source.clip = _endCall;
			_source.Play();	
		}*/
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
