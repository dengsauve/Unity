using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -17)
		{
			Die();
		}
	}

	void Die (){
		SceneManager.LoadScene("Prototype_1");
		/*
		Debug.Log ("Player has fallen to his death!");
		yield return new WaitForSeconds (2);
		Debug.Log ("Player has died.");
		*/
		//yield return null;
	}
}
