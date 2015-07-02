using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sync_Size : MonoBehaviour {

	[SerializeField]Text[] TextTabSync; 
	[SerializeField] Text TargetSync;
	void Update () {
	
		for (int i = 0; i < TextTabSync.Length; i++) {
			TextTabSync [i].fontSize = TargetSync.cachedTextGenerator.fontSizeUsedForBestFit;
		}
	}
}
