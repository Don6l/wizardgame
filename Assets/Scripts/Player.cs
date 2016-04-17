﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//floats
	public float maxSpeed = 1;
	public float speed = 50f;
	public float jumpPower = 150f;

	//booleans
	public bool grounded; 
	public bool canDoubleJump;

	//Ref
	private Rigidbody2D rb2d;
	private Animator anim; 

	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}

	void Update () {
		anim.SetBool("Grounded", grounded);
		anim.SetFloat ("Speed",Mathf.Abs(rb2d.velocity.x));

		if (Input.GetAxis ("Horizontal") < -0.1f) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}

		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3 (1, 1, 1);
		}

		if (Input.GetButtonDown ("Jump")) {

			if (grounded) {
				rb2d.AddForce (Vector2.up * jumpPower);
				grounded = false;
				canDoubleJump = true;
			} else {
				
				if (canDoubleJump) {

					canDoubleJump = false;
					rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
					rb2d.AddForce (Vector2.up * jumpPower);
				}
			}

		}
	}

	void FixedUpdate(){

		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;


		float h = Input.GetAxis ("Horizontal");

		//fake fristionc/ easing the x speed of our player
		rb2d.velocity = easeVelocity;

		//moving player
		rb2d.AddForce ((Vector2.right * speed) * h);


		//Limiting the speed of the player
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}

		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}
}
