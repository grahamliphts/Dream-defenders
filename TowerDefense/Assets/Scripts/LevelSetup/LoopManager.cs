using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour {

    public Text _feedbackText;
    public int _waveNumber;
    public int _ennemyAddedByWave;
 

    private EnnemyManager m_ennemyManager;
    private int _WaveNumberEnnemy;
    private int _ActualWave;
	// Use this for initialization

	void Start () {
        _feedbackText.text = "Wave 1";
        m_ennemyManager = GetComponent<EnnemyManager>();
        _WaveNumberEnnemy = m_ennemyManager.CurrentNumber;
        _ActualWave = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(m_ennemyManager.AllSpawned )
        {
            Debug.Log("All spawned");
        }

        if (m_ennemyManager.AllSpawned && m_ennemyManager.AllDied)
       {
           if (_waveNumber >= _ActualWave)
           {
               _ActualWave++;
               _feedbackText.text = "Wave" + _ActualWave;       
               m_ennemyManager.CurrentNumber = m_ennemyManager.CurrentNumber + _ennemyAddedByWave;
           }
           else
           {
               _feedbackText.text = "You Win";
           }

       }
	}
}
