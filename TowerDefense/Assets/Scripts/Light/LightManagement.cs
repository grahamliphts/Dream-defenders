using UnityEngine;
using System.Collections;

public class LightManagement : MonoBehaviour
{
	private Light _light;
	private float _timer = 0;
	private float _intensity;
	private bool _lighting = false;

	void Start()
	{
		_light = GetComponent<Light>();
		_intensity = _light.intensity;
		_timer = 0;
	}

	void Update()
	{
		float _newIntensity = 0;
		Debug.Log("intensité 0 =  "+_intensity);
		//Debug.Log("Light intensity " + _light.intensity + " _intensity " + _intensity + " new intensity " + _newIntensity);
		//Debug.Log("Lighting " + _lighting);
		if (_light.intensity == 8)
		{
			_lighting = false;
			if(_newIntensity >=5)
				_newIntensity = Random.Range(1, 5);
			if(_newIntensity <5)	
				_newIntensity = Random.Range(5, 8);
		}
		if (_light.intensity > _newIntensity && _lighting == false)
			_light.intensity -= 0.05f;
		else if (_intensity > _light.intensity && _lighting == true)
			_light.intensity += 0.05f;
		if (_light.intensity == _newIntensity)
			_lighting = true;
	}
}
