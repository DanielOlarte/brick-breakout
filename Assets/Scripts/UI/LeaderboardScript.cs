using UnityEngine;
using System.Text;
using System.Collections;

public class LeaderboardScript : MonoBehaviour {
	
	static private tk2dTextMesh leaderboardMesh;
	

	// Use this for initialization
	void Start () {
		leaderboardMesh = GetComponent<tk2dTextMesh>();

		string[] leaderboard = ScoreUtils.getLeaderboard();

		StringBuilder sb = new StringBuilder();

		for (int i = 1; i <= leaderboard.Length; i++) {
			string position = i.ToString();
			position = position.PadRight(18 - position.Length , ' ');
			string[] userData = leaderboard[i - 1].Split('_');
			string playerName = userData[0];
			playerName = playerName.PadRight(19, ' ');
			string playerScore = userData[1];

			sb.Append(position + playerName + playerScore + "\n");  
		}

		leaderboardMesh.text = sb.ToString();
		leaderboardMesh.Commit ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}