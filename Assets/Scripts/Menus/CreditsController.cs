using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

	void backMainMenuClicked() {
		Debug.Log ("backMainMenuClicked");
		Application.LoadLevel ("MainMenu");
	}
}
