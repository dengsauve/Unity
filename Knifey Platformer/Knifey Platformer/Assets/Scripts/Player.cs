using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 200f;

	public bool grounded;
	public bool canDoubleJump;

	public int curHealth;
	public int maxHealth = 5;

	private Rigidbody2D rb2d;
	private Animator anim;

	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();

		curHealth = maxHealth;
	}
	
	void Update () {
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));

		// Flip player logic
		if (Input.GetAxis ("Horizontal") < -0.1f) {
			transform.localScale = new Vector3 (-1, 1, 1);
		} else if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3 (1, 1, 1);
		}

		// Jumping logic
		if (Input.GetButtonDown ("Jump")) {
			if (grounded) {
				rb2d.AddForce (Vector2.up * jumpPower);
				canDoubleJump = true;
			} else {
				if (canDoubleJump) {
					canDoubleJump = false;
					rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
					rb2d.AddForce (Vector2.up * jumpPower * 0.6f);
				}
			}
		}

		// Checking Health
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		else if (curHealth <= 0) {
			Die ();
		}

	}

	void FixedUpdate(){
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		float h = Input.GetAxis ("Horizontal");

		//Fake friction / Easing the x speed of player
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}

		rb2d.AddForce (Vector2.right * speed * h);

		//Limiting the speed of the player.
		if (rb2d.velocity.x > maxSpeed) 
		{
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		} else if (rb2d.velocity.x < -maxSpeed) 
		{
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}

	void Die(){
		//This is potentially deprecated, TODO: Research This
		Application.LoadLevel (Application.loadedLevel);
		//YouTube comments provided following command.
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);﻿

	}


	public void Damage(int damage){

		curHealth -= damage;

	}
}
