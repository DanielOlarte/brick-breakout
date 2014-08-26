using UnityEngine;
using System;
using System.Collections;

public class CounterController : MonoBehaviour {

	public TimeScript timeScript;

	private float timer;
	private tk2dTextMesh textMesh;
	private BallController ballController;

	// Use this for initialization
	void Start () {
		timer = 4.0f;
		textMesh = (tk2dTextMesh)GetComponent<tk2dTextMesh> ();

		ballController = (BallController)FindObjectOfType (typeof(BallController));
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		TimeSpan t = TimeSpan.FromSeconds(timer);

		textMesh.text = string.Format("{0}", t.Seconds);
		textMesh.Commit();

		if (timer <= 0.0f) {
			textMesh.text = "GO!";
			textMesh.Commit ();

			Invoke("startLevel", 1); 
		}
	}

	private void startLevel() {
		timer = 4.0f;
		timeScript.setStarted (true);
		ballController.setStarted (true);

		gameObject.SetActive(false);
	}
}
