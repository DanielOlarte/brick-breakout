using UnityEngine;
using System.Collections;
using System.Text;

public class SummaryScoreMenuController : MonoBehaviour {

	public Transform[] childTransforms;
	public tk2dTextMesh resultsText;
	public AudioClip sound;

	private bool isShowing;

	void Start() {
		isShowing = false;
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
		int currentLevel = StringUtils.getLevelBySceneName(PlayerPrefs.GetString (ScoreUtils.CURRENT_LEVEL_USER));
		int totalLevels = TimerUtils.getNumberOfLevels();
		if (currentLevel < totalLevels) {
			string newLevel = StringUtils.getNextLevelName(PlayerPrefs.GetString (ScoreUtils.CURRENT_LEVEL_USER));
			PlayerPrefs.SetString(ScoreUtils.CURRENT_LEVEL_USER, newLevel);
			Application.LoadLevel ("LevelBase");
		} else {
			Application.LoadLevel ("GameOver");
		}
	}

	private void enableSummaryMenu() {
		isShowing = true;
		SoundManager.GetInstance ().changeAudio (sound);
		foreach (Transform t in childTransforms) {
			t.gameObject.SetActive(true);
		}
		Time.timeScale = 0;
	}

	private void disableSummaryMenu() {
		isShowing = false;
		foreach (Transform t in childTransforms) {
			t.gameObject.SetActive(false);
		}
	}

	public bool isShowingSummary() {
		return isShowing;
	}

}