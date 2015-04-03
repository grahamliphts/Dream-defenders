using UnityEngine;
using System.Collections;

public class CharacController : MonoBehaviour
{
    public float movementSpeed = 4.0f;
    public float jumpSpeed = 4.0f;

    private bool _wantToJump = false;
    private Rigidbody _rigidbody;
    private Vector3 _eulerAngleVelocity = new Vector3(0, 140, 0);

    private NetworkView _networkView;
	private Animator _animator;
	private Vector3 _direction;
	private bool _modeMulti;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
		if (GetComponent<NetworkView>() != null)
		{
			_networkView = GetComponent<NetworkView>();
			_modeMulti = false;
		}
        transform.tag = "player";

		_animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _wantToJump = true;
		if(Input.GetKey(KeyCode.LeftShift))
			movementSpeed = 10.0f;
		else
			movementSpeed = 6.0f;
    }

    void FixedUpdate()
    {
		if (_modeMulti == false || _networkView.isMine)
		{
			_direction = Vector3.zero;
			if (Input.GetKey("z"))
				_direction += -transform.forward; 
			if (Input.GetKey("s"))
				_direction = transform.forward;
			if (Input.GetKey("q"))
				_direction += transform.right;
			if (Input.GetKey("d"))
				_direction -= transform.right;

			_direction.Normalize();
			_direction *= movementSpeed;

			Vector3 groundDir = -Vector3.up;
			float groundDist = 0.2f;
			RaycastHit hit;
			Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), groundDir * groundDist, Color.red);
			if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), groundDir, out hit, groundDist))
			{
				if (_wantToJump)
				{
					_wantToJump = false;
					_direction += transform.up * jumpSpeed;
				}
			}
			_direction.y += _rigidbody.velocity.y;
		}
		_rigidbody.velocity = _direction;
		float dotProduct = Vector3.Dot(_rigidbody.velocity, transform.right);
		Debug.Log(dotProduct);
		Vector3 velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);

		if (dotProduct < -0.1f)
			_animator.SetBool("right", true);
		else if(dotProduct > 0.1f)
			_animator.SetBool("left", true);
		else
		{
			_animator.SetBool("right", false);
			_animator.SetBool("left", false);
		}
			
		if (velocity.magnitude > 0)
			_animator.SetBool("run", true);
		else
			_animator.SetBool("run", false);
       
    }

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 velocity = Vector3.zero;
		if (stream.isWriting)
		{
			velocity = _direction;
			stream.Serialize(ref velocity);
		}
		
		else
		{
			stream.Serialize(ref velocity);
			_direction = velocity;
		}
	}
}