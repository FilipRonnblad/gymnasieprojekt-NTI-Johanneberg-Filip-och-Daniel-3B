using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

	Rigidbody2D body;
	float speed = 0;
    float max_speed = 5;
    float min_speed = -2;
    float acc = 0.5f;
    float acc_vel_less_zero = 1;
    float de_acc = 2;
	float xrotation = 2f;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		body.gravityScale = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {

		// All forward motion and rotation
		if (Input.GetKey(KeyCode.W) == true)
		{
            if (speed < max_speed && speed >= 0)
            {
                speed += acc;
                body.velocity = transform.up * speed;
            }
            if (speed <= 0)
            {
                speed += acc_vel_less_zero;
                body.velocity = transform.up * speed;
            }

			/* Ta bort denna delen den är överflödig
			if (Input.GetKey(KeyCode.A) == true)
			{
				body.MoveRotation(body.rotation + xrotation);
				body.velocity = transform.up * speed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D) == true)
			{
				body.MoveRotation(body.rotation - xrotation);
				body.velocity = transform.up * speed * Time.deltaTime;
			}
			*/

		}

		// Forward rotation without pressin W
		if (Input.GetKey(KeyCode.A) == true && speed>0 && !Input.GetKey(KeyCode.S))
		{
			body.MoveRotation(body.rotation + xrotation);
			body.velocity = transform.up * speed;
		}
		if (Input.GetKey(KeyCode.D) == true && speed>0 && !Input.GetKey(KeyCode.S))
		{
			body.MoveRotation(body.rotation - xrotation);
			body.velocity = transform.up * speed;
		}

		// All backwards motions and rotations
		else if(Input.GetKey(KeyCode.S) == true)
		{
            if (speed > min_speed)
            {
                speed -= de_acc;
                body.velocity = transform.up * speed;
            }

			/* Ta bort denna delen, den är överflödig
			if (Input.GetKey(KeyCode.A) == true && speed<0 && !Input.GetKey(KeyCode.W))
			{
				body.MoveRotation(body.rotation - xrotation);
				body.velocity = transform.up * speed * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D) == true && speed<0 && !Input.GetKey(KeyCode.W))
			{
				body.MoveRotation(body.rotation + xrotation);
				body.velocity = transform.up * speed * Time.deltaTime;
			}
			*/
		}
	
		// Backward rotation without pressing S
		if (Input.GetKey(KeyCode.A) == true && speed < 0 && !Input.GetKey(KeyCode.W))
		{
			body.MoveRotation(body.rotation - xrotation);
			body.velocity = transform.up * speed;
		}
		if (Input.GetKey(KeyCode.D) == true && speed < 0 && !Input.GetKey(KeyCode.W))
		{
			body.MoveRotation(body.rotation + xrotation);
			body.velocity = transform.up * speed;
		}
	}
}
