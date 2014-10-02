using UnityEngine;
using System.Collections;
using System.Text;

public class CreditsController : MonoBehaviour {

	public tk2dTextMesh creditsText;

	void Start() {
		StringBuilder builder = new StringBuilder ();
		builder.Append ("Published by Brainstorm (2014)\n\n");

		builder.Append ("Programmers\n");
		builder.Append ("Juan Sebastian Rodriguez Casas\n");
		builder.Append ("Oscar Daniel Olarte Fuentes\n\n");

		builder.Append ("Graphics\n");
		builder.Append ("Kenney.nl, freepik.com\n\n");

		builder.Append ("Music\n");
		builder.Append ("luckylittleraven, Taira Komori, freesound.org\n\n");

		builder.Append ("Special Thanks\n");
		builder.Append ("2D Toolkit, Cartoon FX Pack 1\n");
		
		creditsText.text = builder.ToString ();
		creditsText.formatting = true;
		creditsText.wordWrapWidth = 430;
		creditsText.Commit ();
	}

	void backMainMenuClicked() {
		Application.LoadLevel ("MainMenu");
	}
}
