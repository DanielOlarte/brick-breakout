using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectionController : MonoBehaviour {

	void levelSelectedClicked(tk2dUIItem item) {
		string levelName = item.name;
		Regex regex = new Regex(@"Level[0-9]+"); // Pattern to look for
		Match match = regex.Match (levelName);

		PlayerPrefs.SetInt (ScoreUtils.TOTAL_SCORE, 0);
		PlayerPrefs.SetInt (ScoreUtils.LIVES, 3);

		if (match.Value.Equals ("Level01") || match.Value.Equals ("Level02")) {
			Application.LoadLevel (match.Value);
		}
	}

	void backMainMenuClicked() {
		Debug.Log ("backMainMenuClicked");
		Application.LoadLevel ("MainMenu");
	}
}
