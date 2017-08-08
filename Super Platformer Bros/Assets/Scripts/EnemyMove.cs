using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	public int enemySpeed;
	public int xMoveDirection;


	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (xMoveDirection, 0));
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (xMoveDirection, 0) * enemySpeed;
		if (hit.distance < 1.5f){
			Flip();
			if (hit.collider.tag == "Player") {
				Destroy (hit.collider.gameObject);
			}
		}
	}

	void Flip(){
		if (xMoveDirection > 0){
			xMoveDirection = -1;
		}else{
			xMoveDirection = 1;
		}
	}
}
