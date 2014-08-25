using UnityEngine;
using System.Collections;
using System.Text;

public class SummaryScoreMenuController : MonoBehaviour {

	public Transform[] childTransforms;
	public tk2dTextMesh resultsText;

	void Start() {
		disableSummaryMenu ();
	}
	
	void Update() {
	}

	public void show(int score, int lives, float leftTime) {
		StringBuilder strBuilder = new StringBuilder ();
		strBuilder.Append ("Score:" + StringUtils.getSpaces(17));
		strBuilder.Append (score + "\n");
		strBuilder.Append ("Lives: "); 
		strBuilder.Append (lives + StringUtils.getSpaces(15));
		strBuilder.Append (ScoreUtils.getLivesScore (lives) + "\n");
		strBuilder.Append ("Time: ");
		strBuilder.Append (TimerUtils.getTimeFromFloat (leftTime) + StringUtils.getSpaces(8));
		strBuilder.Append (ScoreUtils.getTimeScore (leftTime) + "\n");

		int totalScore = ScoreUtils.getFullScore (score, leftTime, lives);
		strBuilder.Append ("Total:" + StringUtils.getSpaces(18));
		strBuilder.Append (totalScore);

		resultsText.text = strBuilder.ToString ();
		resultsText.Commit();
	
		PlayerPrefs.SetInt (ScoreUtils.TOTAL_SCORE, totalScore);
		PlayerPrefs.SetInt (ScoreUtils.LIVES, lives);

		enableSummaryMenu ();
	}

	private void moveNextLevel() {
		int currentLevelIndex = Application.loadedLevel;
		int totalScenes = Application.levelCount;
		if (currentLevelIndex + 1 < totalScenes) {
			Application.LoadLevel (currentLevelIndex + 1);
		} else {
			Application.LoadLevel ("GameOver");
		}
	}

	private void enableSummaryMenu() {
		foreach (Transform t in childTransforms) {
			t.gameObject.SetActive(true);
		}
		Time.timeScale = 0;
	}

	private void disableSummaryMenu() {
		foreach (Transform t in childTransforms) {
			t.gameObject.SetActive(false);
		}
	}
}