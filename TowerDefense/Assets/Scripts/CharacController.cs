using UnityEngine;
using System.Collections;

public class CharacController : MonoBehaviour 
{
	public float movementSpeed = 4.0f;
	public float jumpSpeed = 4.0f;

	private bool _wantToJump = false;
	private Rigidbody _rigidbody;
	private Vector3 _eulerAngleVelocity = new Vector3(0, 100, 0);
    private float _distance = 3.0f;
	void Start()
	{
		_rigidbody = this.GetComponent<Rigidbody>();
        transform.tag = "player";
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			_wantToJump = true;
	}

	void FixedUpdate () 
	{
		Vector3 direction = Vector3.zero; 
		if (Input.GetKey ("z"))
				direction += transform.forward;
		if (Input.GetKey ("s"))
				direction -= transform.forward;
		if (Input.GetKey ("a"))
			direction -= transform.right;
		if (Input.GetKey ("e")) 
			direction += transform.right;

		if (Input.GetKey ("q")) 
		{
			Quaternion deltaRotation = Quaternion.Euler(-_eulerAngleVelocity * Time.deltaTime);
			_rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
		}
		if (Input.GetKey ("d")) 
		{
			Quaternion deltaRotation = Quaternion.Euler(_eulerAngleVelocity * Time.deltaTime);
			_rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
		}

		direction.Normalize ();
		direction *= movementSpeed;

		Vector3 groundDir = -Vector3.up;
		float groundDist = 0.51f;
		RaycastHit hit;
        //Debug.DrawRay(transform.position, groundDir * groundDist, Color.red);
        if (Physics.Raycast(transform.position, groundDir, out hit, groundDist))
		{
			if (_wantToJump)
			{
                Debug.Log("jump");
				_wantToJump = false;
				direction += transform.up * jumpSpeed;
			}
		}

		direction.y += _rigidbody.velocity.y;
		_rigidbody.velocity = direction;
	}


}