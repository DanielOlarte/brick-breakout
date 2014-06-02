using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowBallsObj : MonoBehaviour {
	
	public float timeEffect = 3.0f;
	public float speedModifier = 0.4f;
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
			ballController.setSpeedModifier("slow_speed", -ballController.getModifiedSpeed()*speedModifier);
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
			ballController.removeSpeedModifier("slow_speed");
		}

		Destroy (gameObject);
		Debug.Log ("Effect Slow Done");
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}


