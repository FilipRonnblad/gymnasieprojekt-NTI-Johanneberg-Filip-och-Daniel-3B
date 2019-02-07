using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCookiePhysics : MonoBehaviour {

    public Body body = new Body(1, 9.82f, 0.01f);
    public bool useFriction = true;
    public ICustomCookieCollision cookieCollision;

    private void FixedUpdate()
    {
        if (useFriction) Friction();

        Move();
    }

    void Friction()
    {
        float lostSpeed = (body.frictionCoefficient /* TODO: ADD  + groundFrictionCoefficient*/ ) * body.gravityForce * body.velocity.magnitude; // F*S (NewtonMeter) är inte hastighet!
        RemoveVelocity(lostSpeed);
    }

    void Move()
    {
        transform.position += (Vector3)body.velocity * Time.fixedDeltaTime;
        Debug.Log("(CustomCookieController) Velocity: " + body.velocity.magnitude);
    }

    public void AddVelocity(float metersPerSecond)
    {
        body.velocity += (Vector2)transform.up * metersPerSecond;
    }

    public void AddVelocity(Vector2 metersPerSecond)
    {
        body.velocity += metersPerSecond;
    }

    public void RemoveVelocity(float metersPerSecond)
    {
        if (metersPerSecond < body.velocity.magnitude)
        {
            body.velocity -= metersPerSecond * body.velocity.normalized;
        }
        else
        {
            body.velocity = Vector2.zero;
        }
        Debug.Log("(CustomCookieController) FrictionForce:" + metersPerSecond);
    }

    /*public void Impulse(Vector2 force, Vector2 posOfImpact) // Currently Adds force Instataniously
    {
        Vector2 deltaImpulse = force * 1; // I = F * s
        Vector2 momentum = deltaImpulse; // p = m * v
        Vector2 impactVelocity = deltaImpulse / body.weight; // v = p / m
        
        // TODO: calculate from impact point velocity and rotationVelocity on body.
    }*/

    public struct Body
    {
        public float weight;
        public float gravityConstant;
        public Collider2D collider;
        public Vector2 velocity;

        public float gravityForce {
            get {
                return weight * gravityConstant;
            }
        }

        public float frictionCoefficient;

        public Body(float _weight, float _gravityConstant, float _frictionCoefficient)
        {
            weight = _weight;
            gravityConstant = _gravityConstant;
            frictionCoefficient = _frictionCoefficient;
            collider = new BoxCollider2D();
            velocity = Vector2.zero;
        }

        public void UpdateValues(float newWeight, float newFrictionCoefficient)
        {
            weight = newWeight;
            frictionCoefficient = newFrictionCoefficient;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Add calculation for collision and resulting velocity! (Create a Collision module script)
        if(cookieCollision != null)
        {
            CustomCookiePhysics otherBody = collision.transform.GetComponent<CustomCookiePhysics>();
            if (otherBody != null) // Calculates new velocity after collision
            {
                Vector2 myNewVelocity = cookieCollision.CalculateResultingVelocity(this, otherBody, collision);
                body.velocity = myNewVelocity;
            } else
            {
                Debug.Log("(CustomCookiePhysics) Other object does not contain a CustomCookiePhysics script!");
            }
        } else
        {
            Debug.Log("(CustomCookiePhysics) No Collision module has been assigned!");
        }
    }

    /* TODO:
     * 
     * Add impuls to objects OnCollisionEnter/OnCollision.
     *  * Check if we are going to hit something using raycasts (If so only move us the remaining amount and set velocity to 0 or velocity * bouncyAmount in resulting direction).
     *  * Add a rotation velocity? or add multiple velocities?
     *      * Make a general interface for at collision script for handling collision calculations and returning the resulting velocity.
     *      * Create a collision module who simulates a fake collision.
     *      * Create a collision module who simulates a physics based collision.
     *      
     * Grab frictionCoefficient from ground and add to frictionCoefficient.
     * 
     * 
     */
}
