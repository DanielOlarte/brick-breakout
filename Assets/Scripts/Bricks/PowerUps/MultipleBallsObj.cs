using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultipleBallsObj : MonoBehaviour {

	public int numberBalls;
	public float powerModifier;
	public float limitLowAngle;
	public float limitHighAngle;
	public float scoreModifier = 2.0f;
	
	private string modifierStr = ScoreUtils.MULTIPLE_BALLS_MODIFIER;

	private bool paddleDidntCapture = false;

	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Paddle")) {
			GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
			if (gameManager != null) {
				GetComponent<AudioSource>().Play();
				paddleDidntCapture = true;
				for (int i = 0; i < numberBalls; i++) {
					GameObject ball = (GameObject)Instantiate(Resources.Load("Ball"));
					BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
					ballController.setOnPaddle(false);
					ballController.setStarted(true);
					Vector2 velocity = gameManager.getSpeedBall();
					ball.rigidbody2D.velocity = velocity;

					float randomAngle = Random.Range (limitLowAngle, limitHighAngle);
					Debug.Log (randomAngle);
					Vector3 dir = Quaternion.AngleAxis(randomAngle, Vector3.forward) * Vector3.right;
					ball.rigidbody2D.AddForce(dir*powerModifier, ForceMode2D.Impulse);

					gameManager.addBall(ball);
				}


				gameManager.setScoreModifier (modifierStr, scoreModifier);
				renderer.enabled = false;
			}
		}
	}

	void OnBecameInvisible() {
		if (!paddleDidntCapture) {
			Destroy (gameObject);
		}
	}
}
