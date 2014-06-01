using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private GameObject paddleGO;
	private GameObject ballGO;
	private GameObject timeScriptGO;

	private List<GameObject> listLives;
	private int lives;
	private int numberBricks;

	// Use this for initialization
	void Start () {
		ballGO = (GameObject)Instantiate(Resources.Load("Ball"));
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

	public void updateLivesAndInstantiate() {
		if (numberBricks > 0) {
			lives--;

			Destroy (listLives [lives]);
			if (lives > 0) {
				StartCoroutine (startWaitNextBall (2.0f));
			} else {
				finishGame ();
			}
		}
	}

	public void finishGame() {
		Destroy (ballGO);

		disablePaddle ();
		disableTimer ();
	}

	public void minusBrick() {
		numberBricks--;
	}

	private void disablePaddle() {
		PaddleController paddleController = (PaddleController)paddleGO.GetComponent (typeof(PaddleController));
		paddleController.enabled = false;
	}

	private void disableTimer() {
		TimeScript timeScript = (TimeScript) timeScriptGO.GetComponent(typeof(TimeScript));
		timeScript.enabled = false;
	}

	private IEnumerator startWaitNextBall(float seconds)
	{
		yield return StartCoroutine( waitSeconds(seconds) );
	}
	
	private IEnumerator waitSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ballGO = (GameObject)Instantiate(Resources.Load("Ball"));
	}
}
