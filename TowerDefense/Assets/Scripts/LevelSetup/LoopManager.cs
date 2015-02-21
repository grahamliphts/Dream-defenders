using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour {

    public Text _feedbackText;
    public int _waveNumber;
    public int _ennemyAddedByWave;
    public int _waveWait;

    private EnnemyManager m_ennemyManager;
    private int _WaveNumberEnnemy;
    private int _ActualWave;
    private float _startTime;
	// Use this for initialization

	void Start () {
        _feedbackText.text = "Wave 1";
        m_ennemyManager = GetComponent<EnnemyManager>();
        _WaveNumberEnnemy = m_ennemyManager.CurrentNumber;
        _ActualWave = 0;
        _startTime = Time.time;
        m_ennemyManager.spawn();
	}
	
	// Update is called once per frame
	void Update () 
    {
       
        _feedbackText.text = ((int)(Time.time-_startTime)%60).ToString();
       /*  if (((int)(Time.time - _startTime) % 60) >= _waveWait)
        {
            m_ennemyManager.spawn();
        }*/


        if (m_ennemyManager.AllSpawned && ((int)(Time.time - _startTime) % 60) >= _waveWait)
       {
           if (_waveNumber >= _ActualWave)
           {
               _startTime = Time.time;
              /* _ActualWave++;
               _feedbackText.text = "Wave" + _ActualWave;       
               m_ennemyManager.CurrentNumber = m_ennemyManager.CurrentNumber + _ennemyAddedByWave;
               m_ennemyManager.spawn();*/
           }
           else
           {
               //_feedbackText.text = "You Win";
           }

       }
    }
}
