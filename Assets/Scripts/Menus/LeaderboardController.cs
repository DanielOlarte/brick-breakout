using UnityEngine;
using System.Collections;

public class LeaderboardController : MonoBehaviour {

	void backMainMenuClicked() {
		Debug.Log ("backMainMenuClicked");
		Application.LoadLevel ("MainMenu");
	}
}
