using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	void Start() {
		ScoreUtils.initializeLeaderboards ();
	}

	void startGameClicked() {
		Debug.Log ("StartGameClicked");
		Application.LoadLevel ("LevelSelection");
	}

	void leaderboardsClicked() {
		Application.LoadLevel ("Leaderboards");
	}

	void creditsClicked() {
		Application.LoadLevel ("Credits");
	}

	void exitGameClicked() {
		Application.Quit ();
	}
}