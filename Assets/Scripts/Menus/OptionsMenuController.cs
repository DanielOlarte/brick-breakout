using UnityEngine;
using System.Collections;

public class OptionsMenuController : MonoBehaviour {
	
	public void resetLeaderboads() {
		if (PlayerPrefs.GetInt (ScoreUtils.LEADERBOARD_INIT) == 1) {
			for (int i = 1; i <= ScoreUtils.TOTAL_POSITIONS_LEADERBOARD; i++) {
				string position = ScoreUtils.BASE_STR_LEADERBOARD + i.ToString();
				PlayerPrefs.SetString(position, ScoreUtils.DEFAULT_STR_PLAYER + ScoreUtils.DEFAULT_SCORE_PLAYER);
			}
			PlayerPrefs.SetInt (ScoreUtils.LEADERBOARD_INIT, 1);
		}
	}

	void helpMenuClicked() {
		Application.LoadLevel ("HelpMenu");
	}

	void backMainMenuClicked() {
		Application.LoadLevel ("MainMenu");
	}
}
