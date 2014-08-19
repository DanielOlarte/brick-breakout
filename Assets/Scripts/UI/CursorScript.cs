using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

	private GameObject cursorGO;
	private tk2dTextMesh textMesh;

	// Use this for initialization
	void Start () {

	}

	void onTextChanged() {
		if (textMesh == null || cursorGO == null) {
			cursorGO = GameObject.FindGameObjectWithTag (TagUtils.TAG_CURSOR_INPUT);
			textMesh = GameObject.FindGameObjectWithTag (TagUtils.TAG_INPUT_LEADERBOARD).GetComponent<tk2dTextMesh> ();
		}

		int textLength = textMesh.text.Length;
		if (textLength >= ScoreUtils.TOTAL_CHARACTERS_NAME) {
			cursorGO.renderer.enabled = false;
		} else {
			cursorGO.renderer.enabled = true;		
		}
	}
}
