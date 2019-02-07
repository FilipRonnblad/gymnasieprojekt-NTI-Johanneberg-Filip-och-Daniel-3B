using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceController : MonoBehaviour
{

	Rigidbody2D body;

	float speedForce = 10f;
    float breakForce = 2f;
	float torqueForce = -200f;
	float driftFactorSticky = 0.5f; //När man har kontroll
	float driftFactorSlippy = 0.9f; // När man börjar drifta
	float maxStickyVelocity =1.05f; // Punkten då man börjar drifta
	float gravity = 9.82f;
    float frictionConstant = 0.05f;
	float frictionConstantOffTrack = 0.5f;
	


	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		body.gravityScale = 0.0f;
		
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		var localVelocity = transform.InverseTransformDirection(body.velocity);
		float mass = body.mass;
		if (collision.gameObject.tag == "OffTrack")
		{
			body.AddForce(-(transform.up * mass * gravity * frictionConstantOffTrack) * Mathf.Sign(localVelocity.y), ForceMode2D.Force);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		float xInput = Input.GetAxisRaw("Horizontal");
		float yInput = Input.GetAxisRaw("Vertical");
		float breakInput = Input.GetAxisRaw("Jump");
		var localVelocity = transform.InverseTransformDirection(body.velocity); // Så att vi kan kolla på vilken riktining bilen kör.

		float mass = body.mass;

		//Väljer vilken driftfaktor den ska använda sig av beroende på hastigheten.
		float driftFactor = driftFactorSticky;
		if (RightVelocity().magnitude > maxStickyVelocity)
		{
			driftFactor = driftFactorSlippy;
		}

		//Justerar driften på bilen.
		body.velocity = ForwardVelocity() + RightVelocity() * driftFactor;

		//Lägger till kraften på bilen.
		if (yInput != 0)
		{
			body.AddForce((transform.up * Mathf.Sign(yInput)) * speedForce);
		}

        if (breakInput != 0)
        {
            body.AddForce(-(transform.up*breakForce)*Mathf.Sign(localVelocity.y));
        }

		// Destu snabbare man åker destu snabbare svänger man.
		float tf = Mathf.Lerp(0, torqueForce, body.velocity.magnitude / 2);


		// Ser till så att man svänger åt rätt håll beroende på riktning man åker i.
		if (localVelocity.y > 0)
		{
			body.angularVelocity = xInput * tf;

		}
		else if (localVelocity.y < 0)
		{

			body.angularVelocity = -xInput * tf;
		}

		if (localVelocity.y != 0)
		{
			body.AddForce(-(transform.up*mass*gravity*frictionConstant)*Mathf.Sign(localVelocity.y),ForceMode2D.Force);
		}
	}

	//Returns y movement
	Vector2 ForwardVelocity()
	{
		return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
	}

	//Returns x movement
	Vector2 RightVelocity()
	{
		return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + transform.up);
	}
}