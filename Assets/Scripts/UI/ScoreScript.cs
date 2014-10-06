using UnityEngine;
using System.Collections;
using System.Text;

public class ScoreScript : MonoBehaviour {

	static private tk2dTextMesh textMesh;

	private int score;

	void Start () {
		textMesh = GetComponent<tk2dTextMesh>();
		score = PlayerPrefs.GetInt (ScoreUtils.TOTAL_SCORE);
		updateScore ();
	}

	void Update () {
	}

	public void updateScore(int newScore) {
		score += newScore;
		updateScore ();
	}

	public void updateScore() {
		textMesh.text = buildScore ();
		textMesh.Commit();
	}

	private string buildScore() {
		string pointsScore = score.ToString();
		int lengthScore = pointsScore.Length;
		string baseScore = ScoreUtils.BASE_SCORE;

		StringBuilder aStringBuilder = new StringBuilder(baseScore);
		aStringBuilder.Remove((baseScore.Length - lengthScore) + 1, lengthScore - 1);
		aStringBuilder.Insert((baseScore.Length - lengthScore) + 1, pointsScore);

		return aStringBuilder.ToString ();
	}

	public int getScore() {
		return score;
	}
}
