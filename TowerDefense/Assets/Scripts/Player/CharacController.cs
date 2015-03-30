using UnityEngine;
using System.Collections;

public class CharacController : MonoBehaviour
{
    public float movementSpeed = 4.0f;
    public float jumpSpeed = 4.0f;

    private bool _wantToJump = false;
    private Rigidbody _rigidbody;
    private Vector3 _eulerAngleVelocity = new Vector3(0, 140, 0);

    private bool isWalking = false;
    private NetworkView _networkView;
	private Animation _animation;
	private Vector3 _direction;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
		if (GetComponent<NetworkView>() != null)
			_networkView = GetComponent<NetworkView>();
        transform.tag = "player";

		_animation = GetComponent<Animation>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _wantToJump = true;
        }

		if(Input.GetKey(KeyCode.LeftShift))
			movementSpeed = 10.0f;
		else
			movementSpeed = 6.0f;

		Debug.Log(movementSpeed);
    }

    void FixedUpdate()
    {
		if (_networkView != null )
		{
			if (!_networkView.isMine)
				return;
		}
        isWalking = false;
		_direction = Vector3.zero;
        if (Input.GetKey("z"))
        {
			_direction += -transform.forward; //inverted axis change in progress
			_animation["Armature.000|run"].speed = 1.8f;
			_animation.Play("Armature.000|run", PlayMode.StopAll);
            isWalking = true;
        }
        if (Input.GetKey("s"))
        {
			_direction = transform.forward; //inverted axis change in progress
			_animation["Armature.000|run"].speed = 1f;
			_animation.Play("Armature.000|run", PlayMode.StopAll);
            isWalking = true;
        }
        if (Input.GetKey("a"))
        {
			_direction += transform.right; //inverted axis change in progress
            if (!isWalking)
            {
				_animation["Armature.000|left"].speed = 2f;
				_animation.Play("Armature.000|left", PlayMode.StopAll);
                isWalking = true;
            }
        }
        if (Input.GetKey("e"))
        {

			_direction -= transform.right; //inverted axis change in progress
            if (!isWalking)
            {
				_animation["Armature.000|right"].speed = 2f;
				_animation.Play("Armature.000|right", PlayMode.StopAll);
                isWalking = true;
            }
        } 
        /* JUMP ISSUES TO FIX
        if (Input.GetKey(KeyCode.Space))
        {
            animation["Armature.000|jump"].speed = 0.7f;
            animation.Play("Armature.000|jump", PlayMode.StopAll);
            
        }*/
        if (!isWalking)
        {
			_animation.Play("Armature.000|idle", PlayMode.StopSameLayer);
            isWalking = false;
        }
        if (Input.GetKey("q"))
        {
            Quaternion deltaRotation = Quaternion.Euler(-_eulerAngleVelocity * Time.deltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
        if (Input.GetKey("d"))
        {
            Quaternion deltaRotation = Quaternion.Euler(_eulerAngleVelocity * Time.deltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }

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
		_rigidbody.velocity = _direction;
    }

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 position = Vector3.zero;
		Vector3 velocity = Vector3.zero;
		Quaternion rotation = Quaternion.identity;

		if (stream.isWriting)
		{
			position = transform.position;
			velocity = _rigidbody.velocity;
			rotation = _rigidbody.rotation;
			stream.Serialize(ref position);
			stream.Serialize(ref velocity);
			stream.Serialize(ref rotation);
		}
		
		else
		{
			stream.Serialize(ref position);
			stream.Serialize(ref velocity);
			stream.Serialize(ref rotation);
			transform.position = position;
			_rigidbody.velocity = velocity;
			_rigidbody.rotation = rotation;
		}
	}
}