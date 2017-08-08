using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

	private float timeLeft = 120;
	public int playerScore = 0;
	public GameObject timeLeftUI;
	public GameObject playerScoreUI;


	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft);
		if (timeLeft < 0.1f){
			SceneManager.LoadScene("Prototype_1");
		}

		//Debug.Log(timeLeft);
	}

	void OnTriggerEnter2D(Collider2D trig){
		if (trig.gameObject.name == "EndLevel") {
			CountScore ();
		}
		if (trig.gameObject.name == "Coin") {
			playerScore += 10;
			Destroy(trig.gameObject);
		}
		playerScoreUI.gameObject.GetComponent<Text> ().text = ("Score: " + playerScore);
		//Debug.Log("Reached the end of the level!");
	}

	void CountScore(){
		playerScore = playerScore + (int)(timeLeft * 10);
		Debug.Log(playerScore);
	}
}
