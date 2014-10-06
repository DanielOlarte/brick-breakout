using UnityEngine;
using System.Collections;
using System.Text;

public class HelpMenuController : MonoBehaviour {

	public tk2dTextMesh text;

	void Start() {
		StringBuilder builder = new StringBuilder ();
		builder.Append ("Your goal is to destroy all bricks in each level.");
		builder.Append ("There are multiple bricks, each one have special characteristics.");
		builder.Append ("The blue bricks drops a set of skills.\n\n");

		builder.Append ("Normal                                                      Slow Balls\n");
		builder.Append ("XResistent                                               Fast Balls\n");
		builder.Append ("XXResistent                                             Multiple Balls\n");
		builder.Append ("Undestroyable                                          Inverse Paddle\n\n");

		builder.Append ("In order save your score in the leaderboard, you should start from " +
			"the first level.");

		text.text = builder.ToString ();
		text.formatting = true;
		text.wordWrapWidth = 430;
		text.Commit ();
	}

	void backMainMenuClicked() {
		Application.LoadLevel ("Options");
	}
}
