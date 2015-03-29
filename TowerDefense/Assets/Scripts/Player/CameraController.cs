using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform target;
    public float distCamera;
    public float cameraSpeed = 1f;
    public float smoothTime = 0.3f;

	public Vector3 _offset;

	private float _x = 0.0f;
	private float _y = 0.0f;

    private int _yMinLimit = 0;
    private int _yMaxLimit = 70;

    private Vector3 _smoothTarget;
	private GameObject _player;

	void AdjustCamera()
	{
		RaycastHit hit;
		Vector3 dir = transform.position - target.position;
		dir.Normalize ();
        //Debug.DrawRay(target.position, dir * -distCamera, Color.red);
        int layerMask = ((1 << 8) | (1 << 9) | (1 << 10));
        layerMask = ~layerMask;
        if (Physics.Raycast(target.position, dir, out hit, -distCamera, layerMask))
            _offset.z = -hit.distance;
        else
            _offset.z = distCamera;
            
	}

	void LateUpdate () 
	{
		_x = Input.GetAxis("Mouse X") * cameraSpeed;
		_y = -Input.GetAxis("Mouse Y")* cameraSpeed;

		this.RotateCamera(_x,_y);
		this.RotateCharacter(_x);
		AdjustCamera ();
	}

	void RotateCharacter(float x)
	{
		Debug.Log(_player);
		Vector3 actualEuler = _player.transform.rotation.eulerAngles;
		actualEuler.y += x;
		Quaternion rotation = Quaternion.Euler(actualEuler);
		_player.transform.localRotation = rotation;
	}
	
	void RotateCamera(float x, float y)
	{
		Vector3 actualEuler = transform.rotation.eulerAngles;
		actualEuler.x += y;
		actualEuler.y += x;
		actualEuler.x = ClampAngle(actualEuler.x, _yMinLimit, _yMaxLimit);
		Quaternion rotation = Quaternion.Euler(actualEuler);
        transform.rotation = rotation;

        Vector3 position = transform.forward * _offset.z + target.position;
        position.y += _offset.y;
        transform.position = position;
	}
	
	static float ClampAngle (float angle, float min, float max) 
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}

	public void SetPlayer(GameObject player)
	{
		_player = player;
	}
}
