﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private GameObject paddleGO;
	private List<GameObject> ballsGO;
	private GameObject timeScriptGO;

	private List<GameObject> listLives;
	private int lives;
	private int numberBricks;

	// Use this for initialization
	void Start () {
		ballsGO = new List<GameObject> ();
		ballsGO.Add((GameObject)Instantiate(Resources.Load("Ball")));
		paddleGO = GameObject.Find("Paddle");
		timeScriptGO = GameObject.Find("TimeScore");

		lives = 3;
		listLives = new List<GameObject> ();
		for (int i = 0; i < lives; i++) {
			GameObject live = (GameObject)Instantiate(Resources.Load("Live"));
			Vector3 position = live.transform.position;
			position.x += (i*0.5f);
			live.transform.position = position;
			listLives.Insert(i, live);
		}

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Bricks");
		numberBricks = gos.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (numberBricks == 0) {
			finishGame();
		}
	}

	public List<GameObject> getBalls() {
		return ballsGO;
	}

	public void addBall(GameObject ballGO) {
		ballsGO.Add (ballGO);
	}

	public Vector2 getSpeedBall() {
		if (ballsGO.Count > 0) {
			GameObject ballGO = ballsGO[0];
			return ballGO.rigidbody2D.velocity;
		}

		return new Vector2 ();
	}

	public void updateLivesAndInstantiate(GameObject ballGO) {
		Debug.Log (numberBricks);
		ballsGO.Remove (ballGO);
		Debug.Log ("Balls Left: " + ballsGO.Count + "Lives " + lives);
		if (numberBricks > 0 && ballsGO.Count == 0) {
			lives--;
			
			Destroy (listLives [lives]);
			if (lives > 0 ) {
				disablePowers ();
				StartCoroutine (startWaitNextBall (2.0f));
			} else {
				finishGame ();
			}
		}
	}

	public void finishGame() {
		destroyBalls ();
		disablePaddle ();
		disableTimer ();
		disablePowers ();

		Application.LoadLevel ("LevelSelection");
	}

	public void minusBrick() {
		numberBricks--;
	}

	private void destroyBalls() {
		foreach (GameObject ballGO in ballsGO) {
			Destroy (ballGO);
		}
	}

	private void disablePaddle() {
		PaddleController paddleController = (PaddleController)paddleGO.GetComponent (typeof(PaddleController));

		if (paddleController != null) {
			paddleController.enabled = false;
		}
	}

	private void disableTimer() {
		TimeScript timeScript = (TimeScript) timeScriptGO.GetComponent(typeof(TimeScript));
		timeScript.enabled = false;
	}

	private void disablePowers() {
		GameObject[] powersGO = GameObject.FindGameObjectsWithTag ("Powers");

		foreach(GameObject powerGameObject in powersGO) {
			Destroy (powerGameObject);
		}
	}

	private IEnumerator startWaitNextBall(float seconds)
	{
		yield return StartCoroutine( waitSeconds(seconds) );
	}
	
	private IEnumerator waitSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ballsGO.Add((GameObject)Instantiate(Resources.Load("Ball")));
	}
}
