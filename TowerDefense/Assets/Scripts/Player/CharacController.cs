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
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
		if (GetComponent<NetworkView>() != null)
			_networkView = GetComponent<NetworkView>();
        transform.tag = "player";
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
        Vector3 direction = Vector3.zero;
        if (Input.GetKey("z"))
        {
            direction += -transform.forward; //inverted axis change in progress
            GetComponent<Animation>()["Armature.000|run"].speed = 1.8f;
            GetComponent<Animation>().Play("Armature.000|run", PlayMode.StopAll);
            isWalking = true;
        }
        if (Input.GetKey("s"))
        {
            direction = transform.forward; //inverted axis change in progress
            GetComponent<Animation>()["Armature.000|run"].speed = 1f;
            GetComponent<Animation>().Play("Armature.000|run", PlayMode.StopAll);
            isWalking = true;
        }
        if (Input.GetKey("a"))
        {
            direction += transform.right; //inverted axis change in progress
            if (!isWalking)
            {
                GetComponent<Animation>()["Armature.000|left"].speed = 2f;
                GetComponent<Animation>().Play("Armature.000|left", PlayMode.StopAll);
                isWalking = true;
            }
        }
        if (Input.GetKey("e"))
        {

            direction -= transform.right; //inverted axis change in progress
            if (!isWalking)
            {
                GetComponent<Animation>()["Armature.000|right"].speed = 2f;
                GetComponent<Animation>().Play("Armature.000|right", PlayMode.StopAll);
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
            GetComponent<Animation>().Play("Armature.000|idle",PlayMode.StopSameLayer);
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

        direction.Normalize();
        direction *= movementSpeed;

        Vector3 groundDir = -Vector3.up;
        float groundDist = 0.2f;
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), groundDir * groundDist, Color.red);
        if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), groundDir, out hit, groundDist))
        {
            if (_wantToJump)
            {
                _wantToJump = false;
                direction += transform.up * jumpSpeed;
            }
        }

        direction.y += _rigidbody.velocity.y;
        _rigidbody.velocity = direction;
    }

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 position = Vector3.zero;
		if (stream.isWriting)
		{
			position = transform.position;
			stream.Serialize(ref position);
		}
		else
		{
			stream.Serialize(ref position);
			transform.position = position;
		}
	}
}