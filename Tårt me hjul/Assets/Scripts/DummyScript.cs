using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    #pragma warning disable 0414
    Rigidbody2D body; 
    BoxCollider2D bc;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        body.gravityScale = 0.0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("Dummy y position");
        //Debug.Log(body.position.y);
    }
    // Update is called once per frame
    void Update () {

		if (Input.GetKey(KeyCode.Space))
		{
			body.velocity = new Vector2(0,-1);
		}
	}
}
