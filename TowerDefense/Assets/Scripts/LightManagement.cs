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
		if (timmer >= 1 || m_light.intensity / 10 == m_new_intensity) {
			Debug.Log (timmer);
			timmer = 0;

			Debug.Log(m_new_intensity);
			Debug.Log(m_light.intensity/10);
			m_new_intensity = Random.value;
			while(m_new_intensity > 0.4)
				m_new_intensity = Random.value;
			//m_light.intensity = rand;
		}
		if (m_light.intensity / 10 > m_new_intensity) {
			m_light.intensity -= 0.01f;

		} else {

			m_light.intensity += 0.01f;
			//m_light.intensity = m_light.intensity/2;
		}
	}
}
