using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomCookiePhysics))]
public class CustomCookieController : MonoBehaviour {

    CarData car = new CarData(1, 1, 4, 1f, 0.01f);
    CustomCookiePhysics physics;

    bool drifting {
        get{
            return drifting;
        }
        set{
            drifting = value;
        }
    }

    void Start () {
        physics = GetComponent<CustomCookiePhysics>();
	}
	
	void Update () {
		car.UpdatePlayerInput();
	}

    void FixedUpdate()
    {
        if(car.movementInput.y != 0) physics.AddVelocity(car.accelerationAmount);
        if(car.movementInput.x != 0) Turn();
    }

    void Turn()
    {
        transform.Rotate(Vector3.back, car.rotationAmount * car.movementInput.x); // Kan vara användbart?: var localVelocity = transform.InverseTransformDirection(body.velocity); // Så att vi kan kolla på vilken riktining bilen kör.
        physics.body.velocity = Quaternion.AngleAxis(car.rotationAmount * car.movementInput.x, Vector3.back) * physics.body.velocity;
    }

    struct CarData
    {
        public Vector2 movementInput;
        public bool handBreak;

        public float accelerationAmount;
        public float weight;
        public float rotationAmount;
        public float breakAmount;

        public float tireFrictionCoefficient;

        public CarData(float _force, float _weight, float _rotationAmount, float _breakAmount, float _tireFriction)
        {
            accelerationAmount = _force;
            weight = _weight;
            rotationAmount = _rotationAmount;
            breakAmount = _breakAmount;
            tireFrictionCoefficient = _tireFriction;
            movementInput = Vector2.zero;
            handBreak = false;
        }

        public void SetValues(float newAccelerationAmount, float newBreakAmount, float newTireFriction)
        {
            accelerationAmount = newAccelerationAmount;
            breakAmount = newBreakAmount;
            tireFrictionCoefficient = newTireFriction;
        }

        public void UpdatePlayerInput()
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
            handBreak = (Input.GetAxisRaw("Jump") > 0);
        }
    }
}


/*
 * - CarData.movementInput to store input
 * - Rotate() to rotate
 * - AddVelocity() to move forward/backwards
 * - HandBreak() to lower speed depending on friction
 * Drift boolean, controlled by Break() to modify behaviour of Drive() Velocity().
 * - Friction() to add friction to retract from velocity
 * - LowerSpeed(amount) decrease speed by amount / percent
 * - Move transform Velocity amount on end of frame
 * 
*/