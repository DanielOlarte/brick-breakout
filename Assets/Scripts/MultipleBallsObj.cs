using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultipleBallsObj : MonoBehaviour {

	public int numberBalls = 3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag ("Paddle")) {
			Debug.Log ("CollisionMultipleBallsPaddle-----");
			
			renderer.enabled = false;

			GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
			if (gameManager != null) {
				for (int i = 0; i < numberBalls; i++) {
					GameObject ball = (GameObject)Instantiate(Resources.Load("Ball"));
					BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
					ballController.setOnPaddle(false);
					float randomVelocityX = Random.Range(-3.0F, 3.0F);
					float randomVelocityY = Random.Range(-3.0F, 3.0F);
					ball.rigidbody2D.velocity = new Vector2(randomVelocityX, randomVelocityY);
					gameManager.addBall(ball);
				}
			}
		}
	}
}
