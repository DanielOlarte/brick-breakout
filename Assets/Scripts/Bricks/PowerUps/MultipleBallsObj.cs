using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultipleBallsObj : MonoBehaviour {

	public int numberBalls = 3;
	public float powerModifier = 20.0f;
	public float scoreModifier = 2.0f;
	
	private string modifierStr = ScoreUtils.MULTIPLE_BALLS_MODIFIER;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag ("Paddle")) {
			Debug.Log ("CollisionMultipleBallsPaddle-----");

			GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
			if (gameManager != null) {
				for (int i = 0; i < numberBalls; i++) {
					GameObject ball = (GameObject)Instantiate(Resources.Load("Ball"));
					BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
					ballController.setOnPaddle(false);
					Vector2 velocity = gameManager.getSpeedBall();
					ball.rigidbody2D.velocity = velocity;
					float randomModifierForce = Random.Range (-powerModifier, powerModifier);
					ball.rigidbody2D.AddForce( transform.right * randomModifierForce, ForceMode2D.Impulse );
					gameManager.addBall(ball);
				}

				gameManager.setScoreModifier (modifierStr, scoreModifier);
				renderer.enabled = false;
			}
		}
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
