using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class LevelSelectionController : MonoBehaviour {

	void levelSelectedClicked(tk2dUIItem item) {
		string levelName = item.name;
		Regex regex = new Regex(@"Level[0-9]+");
		Match match = regex.Match (levelName);

		PlayerPrefs.SetInt (ScoreUtils.TOTAL_SCORE, 0);
		PlayerPrefs.SetInt (ScoreUtils.LIVES, ScoreUtils.TOTAL_LIVES);

		Application.LoadLevel ("LevelBase");

		PlayerPrefs.SetString (ScoreUtils.LEVEL_USER_INIT, match.Value);
	}

	void backMainMenuClicked() {
		Application.LoadLevel ("MainMenu");
	}
}
