using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowBallsObj : MonoBehaviour {
	
	public float timeEffect = 3.0f;
	public float speedModifier = 0.4f;
	public float scoreModifier = 0.5f;

	private bool paddleDidntCapture = false;
	private string modifierStr = ScoreUtils.SLOW_BALLS_MODIFIER;
	private GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag ("Paddle")) {
			Debug.Log ("CollisionSlowBalls-----");

			paddleDidntCapture = true;
			renderer.enabled = false;

			StartCoroutine ("startObjectEffect");	
		}
	}
	
	private IEnumerator startObjectEffect()
	{
		List<GameObject> balls = gameManager.getBalls ();

		foreach(GameObject ball in balls) {
			BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
			ballController.setSpeedModifier(modifierStr, -ballController.getModifiedSpeed()*speedModifier);
		}

		gameManager.setScoreModifier (modifierStr, scoreModifier);

		yield return StartCoroutine("waitSeconds");
	}
	
	private IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(timeEffect);
		List<GameObject> balls = gameManager.getBalls ();
		
		foreach(GameObject ball in balls) {
			BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
			ballController.removeSpeedModifier(modifierStr);
		}

		gameManager.removeScoreModifier (modifierStr);

		Destroy (gameObject);
		Debug.Log ("Effect Slow Done");
	}

	void OnBecameInvisible() {
		if (!paddleDidntCapture) {
			Destroy (gameObject);
		}
	}
}

