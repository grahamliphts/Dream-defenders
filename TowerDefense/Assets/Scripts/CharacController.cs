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

    private bool isWalking = false;
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        transform.tag = "player";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _wantToJump = true;
        }
    }

    void FixedUpdate()
    {
        isWalking = false;
        Vector3 direction = Vector3.zero;
        if (Input.GetKey("z"))
        {

            direction += -transform.forward; //inverted axis change in progress
            animation["Armature.000|run"].speed = 1.8f;
            animation.Play("Armature.000|run", PlayMode.StopAll);
            isWalking = true;
        }
        if (Input.GetKey("s"))
        {
            direction = transform.forward; //inverted axis change in progress
            animation["Armature.000|run"].speed = 1f;
            animation.Play("Armature.000|run", PlayMode.StopAll);
            isWalking = true;
        }
        if (Input.GetKey("a"))
        {
            direction += transform.right; //inverted axis change in progress
            if (!isWalking)
            {
                animation["Armature.000|left"].speed = 2f;
                animation.Play("Armature.000|left", PlayMode.StopAll);
                isWalking = true;
            }
        }
        if (Input.GetKey("e"))
        {

            direction -= transform.right; //inverted axis change in progress
            if (!isWalking)
            {
                animation["Armature.000|right"].speed = 2f;
                animation.Play("Armature.000|right", PlayMode.StopAll);
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
            animation.Play("Armature.000|idle",PlayMode.StopSameLayer);
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
       /* int layerMask = 1 << 8;
        layerMask = ~layerMask;*/
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


}