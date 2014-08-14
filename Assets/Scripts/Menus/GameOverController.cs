using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	private GameObject[] buttonsGO;
	private GameObject[] buttonsPlayerNameGO;

	void Start() {
		tk2dTextMesh scoreTitle =(tk2dTextMesh) GameObject.Find(NameUtils.NAME_SCORE_TITLE).GetComponent<tk2dTextMesh>();
		scoreTitle.text = PlayerPrefs.GetInt (ScoreUtils.TOTAL_SCORE).ToString ();
		scoreTitle.Commit ();
		buttonsGO = GameObject.FindGameObjectsWithTag (TagUtils.TAG_BUTTONS);
		
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

		string nameUser = ScoreUtils.DEFAULT_STR_PLAYER_NAME;

		buttonsPlayerNameGO = GameObject.FindGameObjectsWithTag (TagUtils.TAG_SCORE_LEADERBOARD);

		foreach(GameObject buttonGO in buttonsPlayerNameGO) {
			if (buttonGO.name.Equals (NameUtils.NAME_INPUT_LEADERBOARD)) {
				tk2dUITextInput textInput = buttonGO.GetComponent<tk2dUITextInput>();
				nameUser = textInput.Text;

				if (nameUser.Length < ScoreUtils.TOTAL_CHARACTERS_NAME) {
					Debug.Log (nameUser);
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
