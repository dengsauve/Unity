using UnityEngine;
using System.Collections;

public class Player_Move_Prot : MonoBehaviour {

	public int playerSpeed = 10;
	private bool facingRight = false;
	public int playerJumpPower = 1250;
	private float moveX;
	public bool isGrounded;

	// Update is called once per frame
	void Update () {
		PlayerMove();
		PlayerRaycast ();
	}

	void PlayerMove(){
		//Controls
		moveX = Input.GetAxis("Horizontal");
		if (Input.GetButtonDown("Jump") && isGrounded){
			Jump();
		}

		//Animation

		//Player Direction
		if (moveX < 0.0f && facingRight == false)
		{
			FlipPlayer();
		}
		else if (moveX > 0.0f && facingRight == true)
		{
			FlipPlayer();
		}

		//Physics
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	//Jumping Code
	void Jump()
	{
		GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);
		isGrounded = false;
	}

	void FlipPlayer(){
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	void OnCollisionEnter2D (Collision2D col){

	}

	void PlayerRaycast () {
		//Ray UP
		RaycastHit2D rayUp = Physics2D.Raycast (transform.position, Vector2.up);
		if (rayUp != null && rayUp.collider != null) {
			if (rayUp.distance < 3.1f && rayUp.collider.name == "Box_2") {
				Destroy (rayUp.collider.gameObject);
			}
		}

		//Ray DOWN
		RaycastHit2D rayDown = Physics2D.Raycast (transform.position, Vector2.down);
		if (rayDown != null && rayDown.collider != null) {
			if (rayDown.distance < 3.1f && rayDown.collider.tag == "Enemy") {
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 10000);
				rayDown.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 450);
				rayDown.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 600);
				rayDown.collider.gameObject.GetComponent<Rigidbody2D> ().freezeRotation = false;
				rayDown.collider.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 8;
				rayDown.collider.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
				rayDown.collider.gameObject.GetComponent<EnemyMove> ().enabled = false;
				//Destroy (hit.collider.gameObject);
			}
			if (rayDown.distance < 3.1f && rayDown.collider.tag != "Enemy") {
				isGrounded = true;
			}
		}
	}
}
