using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform target;
    public float distCamera;
    public Rigidbody targetRigidbody;
    public float cameraSpeed = 1f;

	private Vector3 _offset;

	private float _x = 0.0f;
	private float _y = 0.0f;

    private int _yMinLimit = 0;
    private int _yMaxLimit = 70;

	void Start() 
	{
        _offset = new Vector3(transform.localPosition.x, transform.localPosition.y, distCamera);
	}

	void AdjustCamera()
	{
		RaycastHit hit;
		Vector3 dir = transform.position - target.position;
        
		dir.Normalize ();
        Debug.Log(dir);
        Debug.DrawRay(target.position, dir * -distCamera, Color.red, 5);
        if (Physics.Raycast(target.position, dir, out hit, -distCamera))
        {
            Debug.Log("Raycast");
           if( hit.transform.tag != "player")
                _offset.z = -hit.distance;
        }
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
		Vector3 actualEuler = target.rotation.eulerAngles;
		actualEuler.y += x;
		Quaternion rotation = Quaternion.Euler(actualEuler);
		target.rotation = rotation;
	}
	
	void RotateCamera(float x, float y)
	{
		Vector3 actualEuler = transform.rotation.eulerAngles;
		actualEuler.x += y;
		actualEuler.y += x;
		actualEuler.x = ClampAngle(actualEuler.x, _yMinLimit, _yMaxLimit);

		Quaternion rotation = Quaternion.Euler(actualEuler);
		Vector3 position = rotation * _offset + target.position;
		
		transform.rotation = rotation;
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
}
