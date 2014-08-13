using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	private GameObject[] buttonsGO;
	private GameObject[] buttonsPlayerNameGO;

	void Start() {
		tk2dTextMesh scoreTitle =(tk2dTextMesh) GameObject.Find("ScoreTitle").GetComponent<tk2dTextMesh>();
		scoreTitle.text = PlayerPrefs.GetInt (ScoreUtils.TOTAL_SCORE).ToString ();
		scoreTitle.Commit ();
		buttonsGO = GameObject.FindGameObjectsWithTag ("Buttons");
		
		foreach(GameObject buttonGO in buttonsGO) {
			buttonGO.SetActive(false);
		}
	}

	void mainMenuClicked() {
		Application.LoadLevel ("MainMenu");
	}
	
	void leaderboardsClicked() {
		Application.LoadLevel ("Leaderboards");
	}
	
	void levelSelectionClicked() {
		Application.LoadLevel ("LevelSelection");
	}

	void saveButtonClicked() {

		string nameUser = "AAA";

		buttonsPlayerNameGO = GameObject.FindGameObjectsWithTag ("ScoreLeaderboard");

		foreach(GameObject buttonGO in buttonsPlayerNameGO) {
			if (buttonGO.name.Equals ("InputPlayerName")) {
				tk2dUITextInput textInput = buttonGO.GetComponent<tk2dUITextInput>();
				nameUser = textInput.Text;
				Debug.Log (nameUser);
				if (nameUser.Length < 3) {
					return;
				}

				nameUser = nameUser.ToUpper();
			}
		}

		foreach(GameObject buttonGO in buttonsGO) {
			buttonGO.SetActive(true);
		}

		foreach(GameObject buttonGO in buttonsPlayerNameGO) {
			buttonGO.SetActive(false);
		}

		string[] leaderboard = ScoreUtils.getLeaderboard ();
		int score = PlayerPrefs.GetInt (ScoreUtils.TOTAL_SCORE);
		int position = ScoreUtils.checkUserEnterLeaderboard (leaderboard, score);

		if (position >= 0) {
			ScoreUtils.addUserLeaderboard(nameUser, score, position, leaderboard);
		}


	}
}
