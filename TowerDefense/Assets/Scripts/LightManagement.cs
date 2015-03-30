using UnityEngine;
using System.Collections;

public class LightManagement : MonoBehaviour {
	[SerializeField] Light m_light;
	private float m_new_intensity;
	private float timmer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//m_light.intensity = 0;

		timmer += Time.deltaTime;
		if (timmer >= 1) {
			Debug.Log (timmer);
			timmer = 0;
			m_new_intensity = Random.value;
			Debug.Log(m_new_intensity);
			Debug.Log(m_light.intensity/10);
			//m_light.intensity = rand;
		}
		if (m_light.intensity / 10 > m_new_intensity) {
			m_light.intensity -= 0.05f;
			if(m_light.intensity > 4)
				m_light.intensity =m_light.intensity/2;
		} else {
			if(m_light.intensity > 4)
				m_light.intensity =m_light.intensity/2;
			m_light.intensity += 0.05f;
			//m_light.intensity = m_light.intensity/2;
		}
	}
}
