using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowBallsObj : MonoBehaviour {
	
	public float timeEffect = 3.0f;
	public float speedModifier = 0.5f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag ("Paddle")) {
			Debug.Log ("CollisionSlowBalls-----");
			
			renderer.enabled = false;
			
			StartCoroutine ("startObjectEffect");	
		}
	}
	
	private IEnumerator startObjectEffect()
	{
		GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
		List<GameObject> balls = gameManager.getBalls ();

		foreach(GameObject ball in balls) {
			BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
			ballController.setSpeed(speedModifier);
		}

		yield return StartCoroutine("waitSeconds");
	}
	
	private IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(timeEffect);
		GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
		List<GameObject> balls = gameManager.getBalls ();
		
		foreach(GameObject ball in balls) {
			BallController ballController =  (BallController)ball.GetComponent (typeof(BallController));
			ballController.resetSpeed();
		}

		Destroy (gameObject);
		Debug.Log ("Effect Slow Done");
	}
}


