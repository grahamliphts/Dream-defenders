using UnityEngine;
using System.Collections;

public class LightManagement : MonoBehaviour
{
	private Light _light;
	private float _newIntensity;

	void Start()
	{
		_light = GetComponent<Light>();
	}

	void Update()
	{
		if (_light.intensity == 8)
			_newIntensity = 5;
		else if(_light.intensity > 4 && _light.intensity < 5)
			_newIntensity = 8;
		if (_light.intensity > _newIntensity)
			_light.intensity -= 0.05f;
		else if (_newIntensity > _light.intensity)
			_light.intensity += 0.05f;
	}
}
