using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DanielsPlayer : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 0;
    public float breakConst = 0.97f;
    public float maxSpeed = 10;
    public float minSpeed = -2;
    public float acceleration = 0.2f;
    public float deceleration = 0.2f;
    public float rotationSpeed = 2f;

    float friction = 0.01f;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");  // Keyboard input A : D
        float vInput = Input.GetAxis("Vertical");       // W : S
        float breakInput = Input.GetAxisRaw("Jump");    // Spacebar

        // Acceleration/Deceleration
        if (vInput != 0)
        {
            if (speed < maxSpeed && speed > minSpeed || Mathf.Sign(speed) != Mathf.Sign(vInput))
            {
                speed += ((vInput > 0) ? acceleration : deceleration) * vInput;
            }
        }

        // Rotation Right/Left
        if (hInput != 0)
        {
            rb2d.MoveRotation(rb2d.rotation + rotationSpeed * -hInput * Mathf.Sign(speed));
        }

        // Spacebar/Breaking
        if (breakInput != 0)
        {
            speed *= (speed > 0.01f) ? breakConst : 0;
        }

        speed *= 1 - friction;
        rb2d.velocity = transform.up * speed;
    }

    public float Friction { // TODO: Underlaget ska kunna ändra på friction
        get {
            return friction;
        }
        set {
            friction = value;
        }
    }
}

