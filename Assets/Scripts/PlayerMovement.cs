using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] protected float maxSpeed = 5;
    [SerializeField] protected float maxAccel = 5;

    private Rigidbody _rb;
    private float maxDecel = 8f;
    

// Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var camera = Camera.main;

        Vector3 fwd = camera.transform.forward;
        Vector3 right = camera.transform.right;

        fwd.Normalize();
        right.Normalize();

        Vector3 dir = fwd * vertical + right * horizontal;

        Vector3 targetVelocity = dir * maxSpeed;

        Vector3 deltaVelocity = targetVelocity - _rb.velocity;

        Vector3 vel;
        // If stopping or reversing
        if (Vector3.Dot(deltaVelocity, _rb.velocity) < 0f)
        {
            var acc = deltaVelocity / Time.deltaTime;
            acc = Vector3.ClampMagnitude(acc, maxDecel);
            _rb.AddForce(acc, ForceMode.Acceleration);
            vel = acc;
        }
        else
        {
            _rb.AddForce(dir * maxAccel, ForceMode.Acceleration);
            vel = dir * maxAccel;
        }
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
    }
}
