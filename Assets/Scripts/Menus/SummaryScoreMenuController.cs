using UnityEngine;
using System.Collections;
using System.Text;

public class SummaryScoreMenuController : MonoBehaviour {

	public Transform[] childTransforms;
	public tk2dTextMesh resultsText;
	public GameObject particleSummary;
	public AudioClip sound;

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
		int currentLevel = StringUtils.getLevelBySceneName(PlayerPrefs.GetString (ScoreUtils.LEVEL_USER_INIT));
		int totalLevels = TimerUtils.getNumberOfLevels();
		if (currentLevel < totalLevels) {
			Application.LoadLevel ("LevelBase");
		} else {
			Application.LoadLevel ("GameOver");
		}
	}

	private void enableSummaryMenu() {

		//instantiateParticle ();
		LaunchParticlePoof ();
		SoundManager.GetInstance ().changeAudio (sound);
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

	private void instantiateParticle() {
		InvokeRepeating("LaunchParticlePoof", 5, 1.0F);
	}

	private void LaunchParticlePoof() {
		Vector3 positionHit = transform.position;
		positionHit.z = -10;
		
		//Instantiate(particleSummary, positionHit, Quaternion.identity);

	}
}