using UnityEngine;
using System.Collections;
using System.Text;

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
		int l = pointsScore.Length;
		string baseS = ScoreUtils.BASE_SCORE;
		Debug.Log ("BaseL: " + baseS.Length + " Score: " + l);

		StringBuilder aStringBuilder = new StringBuilder(baseS);
		aStringBuilder.Remove((baseS.Length - l) + 1, l - 1);
		aStringBuilder.Insert((baseS.Length - l) + 1, pointsScore);
		textMesh.text = aStringBuilder.ToString();
		textMesh.Commit();
	}

	public void updateScore() {
		string pointsScore = score.ToString();
		int l = pointsScore.Length;
		string baseS = ScoreUtils.BASE_SCORE;
		Debug.Log ("BaseL: " + baseS.Length + " Score: " + l);
		
		StringBuilder aStringBuilder = new StringBuilder(baseS);
		aStringBuilder.Remove((baseS.Length - l) + 1, l - 1);
		aStringBuilder.Insert((baseS.Length - l) + 1, pointsScore);
		textMesh.text = aStringBuilder.ToString();
		textMesh.Commit();
	}

	public int getScore() {
		return score;
	}
}
