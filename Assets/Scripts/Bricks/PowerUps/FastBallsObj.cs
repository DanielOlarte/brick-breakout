using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FastBallsObj : MonoBehaviour {
	
	public float timeEffect;
	public float speedModifier;
	public float scoreModifier;

	public GameObject particle;

	private bool paddleDidntCapture = false;
	private string modifierStr = ScoreUtils.FAST_BALLS_MODIFIER;
	private GameManager gameManager;
	
	void Start () {
		gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
	}

	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag (TagUtils.TAG_PADDLE)) {
			GetComponent<AudioSource>().Play();
			StartCoroutine ("startObjectEffect");	
		}
	}
	
	private IEnumerator startObjectEffect()
	{
		List<GameObject> balls = gameManager.getBalls ();
		
		foreach(GameObject ball in balls) {
			BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
			ballController.setPowerUp(modifierStr, gameObject, speedModifier);

			Vector3 positionBall = ball.transform.position;
			positionBall.z = -2;
			Quaternion rotationBall = ball.transform.rotation;
			rotationBall.x = 270;

			GameObject particleObject = Instantiate(particle, positionBall, 
			                              			Quaternion.Euler (rotationBall.x, rotationBall.y, rotationBall.z)) 
										  			as GameObject;
			particleObject.transform.parent = ball.transform;
		}

		gameManager.setScoreModifier (modifierStr, scoreModifier);

		paddleDidntCapture = true;
		renderer.enabled = false;

		yield return StartCoroutine("waitSeconds");
	}
	
	private IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(timeEffect);
		List<GameObject> balls = gameManager.getBalls ();
		
		foreach(GameObject ball in balls) {
			BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
			ballController.removePowerUp(modifierStr);
		}

		gameManager.removeScoreModifier (modifierStr);

		Destroy (gameObject);
	}

	void OnBecameInvisible() {
		if (!paddleDidntCapture) {
			Destroy (gameObject);
		}
	}
}

