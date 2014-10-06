using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultipleBallsObj : MonoBehaviour {

	public int numberBalls;
	public float powerModifier;
	public float limitLowAngle;
	public float limitHighAngle;
	public float scoreModifier;

	public GameObject particle;

	private string modifierStr = ScoreUtils.MULTIPLE_BALLS_MODIFIER;

	private bool paddleDidntCapture = false;

	void Start () {

	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag (TagUtils.TAG_PADDLE)) {
			GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
			if (gameManager != null) {
				GetComponent<AudioSource>().Play();
				paddleDidntCapture = true;

				for (int i = 0; i < numberBalls; i++) {
					GameObject ball = (GameObject)Instantiate(Resources.Load(NameUtils.GO_BALL));
					BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
					ballController.setOnPaddle(false);
					ballController.setStarted(true);

					Vector2 velocity = gameManager.getSpeedBall();
					ball.rigidbody2D.velocity = velocity;

					float randomAngle = Random.Range (limitLowAngle, limitHighAngle);
					Vector3 dir = Quaternion.AngleAxis(randomAngle, Vector3.forward) * Vector3.right;
					ball.rigidbody2D.AddForce(dir*powerModifier, ForceMode2D.Impulse);
					gameManager.addBall(ball);
				}

				foreach(GameObject ball in gameManager.getBalls()) {
					Vector3 positionBall = ball.transform.position;
					positionBall.z = -4;
					Quaternion rotationBall = ball.transform.rotation;
					rotationBall.x = 270;
					
					GameObject particleObject = Instantiate(particle, positionBall, 
					                                        Quaternion.Euler (rotationBall.x, rotationBall.y, rotationBall.z))
															as GameObject;
					particleObject.transform.parent = ball.transform;
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
