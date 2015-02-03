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
        if (Input.GetKey("z"))
        {
            direction += -transform.forward; //inverted axis change in progress

			//animation.CrossFade("Armature.000|Run Action 001");
			animation.Play("Armature.000|Run Action.001");
			//animation.Play("nours_rebirth");
			//animation.
            //animation.Play("walk");
        }
		if (Input.GetKey ("s"))
			direction = transform.forward; //inverted axis change in progress
		if (Input.GetKey ("a"))
			direction += transform.right; //inverted axis change in progress
		if (Input.GetKey ("e")) 
			direction -= transform.right; //inverted axis change in progress

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
		float groundDist = 0.2f;
		RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0) , groundDir * groundDist, Color.red);
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), groundDir, out hit, groundDist))
		{
            Debug.Log(hit.transform.tag);
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