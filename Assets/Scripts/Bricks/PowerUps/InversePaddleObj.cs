﻿using UnityEngine;
using System.Collections;

public class InversePaddleObj : MonoBehaviour {

	public float timeEffect;
	public float scoreModifier = 4.0f;

	public GameObject particleInverse;

	private bool paddleDidntCapture = false;
	private string modifierStr = ScoreUtils.INVERSE_PADDLE_MODIFIER;
	private GameManager gameManager;
	private PaddleController paddleController;

	// Use this for initialization
	void Start () {
		gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable() {
		if (paddleController != null) {
			paddleController.inverseDirection ();	
		}
	}

	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag ("Paddle")) {
			Debug.Log ("CollisionInversePaddle-----");
			GetComponent<AudioSource>().Play();
			paddleDidntCapture = true;
			renderer.enabled = false;
			StartCoroutine ("startObjectEffect");	
		}
	}

	private IEnumerator startObjectEffect()
	{
		paddleController = (PaddleController) GameObject.Find("Paddle").GetComponent(typeof(PaddleController));
		paddleController.inverseDirection();

		
		Vector3 p = paddleController.transform.position;
		p.z = -2;
		Quaternion rs = paddleController.transform.rotation;
		rs.x = 270;
		
		GameObject fire = Instantiate(particleInverse, p, Quaternion.Euler (rs.x, rs.y, rs.z)) as GameObject;
		fire.transform.parent = paddleController.transform;

		gameManager.setScoreModifier (modifierStr, scoreModifier);

		yield return StartCoroutine("waitSeconds");
	}
	
	private IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(timeEffect);

		gameManager.removeScoreModifier (modifierStr);

		Destroy (gameObject);
		Debug.Log ("Effect Inverse Done");
	}

	void OnBecameInvisible() {
		if (!paddleDidntCapture) {
			Destroy (gameObject);
		}
	}
}
