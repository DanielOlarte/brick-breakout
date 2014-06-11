using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	void Start() {
		tk2dTextMesh scoreTitle =(tk2dTextMesh) GameObject.Find("ScoreTitle").GetComponent<tk2dTextMesh>();
		scoreTitle.text = PlayerPrefs.GetInt (ScoreUtils.TOTAL_SCORE).ToString ();
		scoreTitle.Commit ();
		/*GameObject[] buttonsGO = GameObject.FindGameObjectsWithTag ("Buttons");
		
		foreach(GameObject buttonGO in buttonsGO) {
			buttonGO.SetActive(false);
		}*/
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
}
