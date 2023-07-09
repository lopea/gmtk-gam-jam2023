using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] protected float maxSpeed = 3;
    [SerializeField] protected float sprintSpeed = 8;
    [SerializeField] protected float maxAccel = 50;
    [SerializeField] protected float turnSpeed = 100;
    [SerializeField] protected float sprintDeaccel = 12f;

    private float Speed;


    private Rigidbody _rb;
    private float maxDecel = 8f;
    private Vector3 scale;

    private int[] directions = { 0, 1, 2, 3 };

    private SplashSoundMaker _splash;

    private bool willSplash = false;
// Awake is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _splash = GetComponent<SplashSoundMaker>();
        scale = transform.localScale;
        Speed = maxSpeed;
    }

    private void Update()
    {
        //vertical is 270
        //horizontal is 180
        

        //Flip
        if (_rb.velocity.x < 0)
            transform.localScale = new Vector3(scale.x, scale.y, -scale.z);
        else
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);

        if (_rb.velocity.magnitude > 3.0f)
        {
            if (Input.GetMouseButton(0))
            {
                transform.rotation = Quaternion.LookRotation(_rb.velocity) * Quaternion.Euler(0, 90, 10);
                
            }
            else if (Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.LookRotation(_rb.velocity) * Quaternion.Euler(0, 90, -20);
            }
            else
                transform.rotation = Quaternion.LookRotation(_rb.velocity) * Quaternion.Euler(0, 90, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (willSplash)
            {
                _splash.Splash();
                willSplash = !willSplash;
            }
            willSplash = !willSplash;
        }
        else if(Input.GetMouseButtonDown(1)){
            if (willSplash)
            {
                _splash.Splash();
                willSplash = !willSplash;
            }
            willSplash = !willSplash;

        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (Speed < sprintSpeed)
                Speed += 1;
        }

        if (Speed > maxSpeed && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            Speed -= sprintDeaccel * Time.deltaTime;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //var camera = Camera.main;

        Vector3 fwd = new Vector3(0,0,1);
        Vector3 right = new Vector3(1,0,0);

        fwd.Normalize();
        right.Normalize();

        Vector3 dir = fwd * vertical + right * horizontal;

        Vector3 targetVelocity = dir * Speed;

        Vector3 deltaVelocity = targetVelocity - _rb.velocity;

        Vector3 vel;
        // If stopping or reversing
        if (Vector3.Dot(deltaVelocity, _rb.velocity) < 0f)
        {
            var acc = deltaVelocity / Time.deltaTime;
            acc = Vector3.ClampMagnitude(acc, turnSpeed);
            _rb.AddForce(acc, ForceMode.Acceleration);
            vel = acc;
        }
        else
        {
            _rb.AddForce(dir * maxAccel, ForceMode.Acceleration);
            vel = dir * maxAccel;
        }

        //if our speed is above max speed and no button has been clicked recently, slow down
        //On clicking left and right increase max speed up to sprinting speed



    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, transform.position + _rb.velocity * 10.0f);
    }
}

