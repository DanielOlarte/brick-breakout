using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

	static private tk2dTextMesh textMesh;

	private int score;
	// Use this for initialization
	void Start () {
		textMesh = GetComponent<tk2dTextMesh>();

		score = PlayerPrefs.GetInt (ScoreUtils.TOTAL_SCORE);
		Debug.Log (score);

		updateScore ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void updateScore(int newScore) {
		score += newScore;
		string pointsScore = score.ToString();
		textMesh.text = "Score: " + pointsScore;
		textMesh.Commit();
	}

	public void updateScore() {
		string pointsScore = score.ToString();
		textMesh.text = "Score: " + pointsScore;
		textMesh.Commit();
	}

	public int getScore() {
		return score;
	}
}
