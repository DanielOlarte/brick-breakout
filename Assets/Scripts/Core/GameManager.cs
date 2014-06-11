using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public float levelScoreModifier = 0.1f;

	private float basicScoreModifier;

	private GameObject paddleGO;
	private List<GameObject> ballsGO;
	private GameObject timeScriptGO;
	private ScoreScript scoreScript;

	private List<GameObject> listLives;
	private int score;
	private int lives;
	private int numberBricks;
	private Dictionary<string, float> scoreModifiers = new Dictionary<string, float>();

	public void setScoreModifier(string key,float value)
	{
		if( !scoreModifiers.ContainsKey(key) )
		{
			scoreModifiers.Add(key,value);
		}
		else{
			scoreModifiers[key] = value;
		}
	}
	
	public void removeScoreModifier(string key)
	{
		scoreModifiers.Remove (key);
	}


	// Use this for initialization
	void Start () {
		Debug.Log (Application.loadedLevelName);

		ballsGO = new List<GameObject> ();
		ballsGO.Add((GameObject)Instantiate(Resources.Load("Ball")));
		paddleGO = GameObject.Find("Paddle");
		timeScriptGO = GameObject.Find("TimeScore");
		scoreScript = (ScoreScript) GameObject.Find("PointsScore").GetComponent(typeof(ScoreScript));

		lives = PlayerPrefs.GetInt(ScoreUtils.LIVES);
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

		int levelNumber = StringUtils.getLevelBySceneName (Application.loadedLevelName);
		basicScoreModifier = levelScoreModifier * (levelNumber - 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (numberBricks == 0) {
			moveNextLevel();
		}
	}
	
	public List<GameObject> getBalls() {
		return ballsGO;
	}

	public void addScore(int points) {
		float modifier = scoreModifiers.Count == 0 ? 1.0f + basicScoreModifier : basicScoreModifier;
		foreach(KeyValuePair<string, float> entry in scoreModifiers)
		{
			modifier += entry.Value;
		}

		Debug.Log ("Modifier : " + modifier);
		scoreScript.updateScore ((int)Mathf.Round(points*modifier));
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
		ballsGO.Remove (ballGO);

		if (ballsGO.Count == 1) {
			removeScoreModifier(ScoreUtils.MULTIPLE_BALLS_MODIFIER);
		}
		
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

		PlayerPrefs.SetInt (ScoreUtils.TOTAL_SCORE, scoreScript.getScore());
		Application.LoadLevel ("GameOver");
	}

	public void moveNextLevel() {
		destroyBalls ();
		disablePaddle ();
		disableTimer ();
		disablePowers ();
		
		PlayerPrefs.SetInt (ScoreUtils.TOTAL_SCORE, scoreScript.getScore());
		PlayerPrefs.SetInt (ScoreUtils.LIVES, lives);

		StartCoroutine (startWaitNextLevel (2.0f));
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

	private IEnumerator startWaitNextLevel(float seconds)
	{
		yield return StartCoroutine( waitSecondsNextLevel(seconds) );
	}
	
	private IEnumerator waitSecondsNextLevel(float seconds)
	{
		yield return new WaitForSeconds(seconds);

		int currentLevelIndex = Application.loadedLevel;
		int totalScenes = Application.levelCount;
		if (currentLevelIndex + 1 < totalScenes) {
			Application.LoadLevel (currentLevelIndex + 1);
		} else {
			Application.LoadLevel ("GameOver");
		}
	}
	
}
