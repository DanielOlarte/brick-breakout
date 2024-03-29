using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public static class ScoreUtils
{
	public static string LEADERBOARD_INIT = "initialize_leaderboard";
	public static string BASE_STR_LEADERBOARD = "BR_";
	public static string DEFAULT_STR_PLAYER_NAME = "AAA";
	public static string DEFAULT_STR_PLAYER = "AAA_";
	public static string DEFAULT_SCORE_PLAYER = "0";

	public static int TOTAL_CHARACTERS_NAME = 3;
	public static int TOTAL_POSITIONS_LEADERBOARD = 10;

	public static string TOTAL_SCORE = "total_score";
	public static string LIVES = "lives";

	public static int TOTAL_LIVES = 3;

	public static string LEVEL_USER_INIT = "level_user_init";
	public static string CURRENT_LEVEL_USER = "current_level_user";

	public static string FAST_BALLS_MODIFIER = "fast_balls";
	public static string SLOW_BALLS_MODIFIER = "slow_balls";
	public static string INVERSE_PADDLE_MODIFIER = "inverse_paddle";
	public static string MULTIPLE_BALLS_MODIFIER = "multiple_balls";
	public static string TIME_MODIFIER = "time_modifier";

	public static float SCORE_LEVEL_MODIFIER = 0.1f;

	public static int SCORE_PER_LIVE = 1000;
	public static int SCORE_PER_SECOND = 10;

	public static string BASE_SCORE = "00000000";

	public static void initializeLeaderboards() {
		if (!PlayerPrefs.HasKey (LEADERBOARD_INIT)) {
			PlayerPrefs.SetInt (LEADERBOARD_INIT, 0);
		}				

		if (PlayerPrefs.GetInt (LEADERBOARD_INIT) == 0) {
			for (int i = 1; i <= TOTAL_POSITIONS_LEADERBOARD; i++) {
				string position = BASE_STR_LEADERBOARD + i.ToString();
				PlayerPrefs.SetString(position, DEFAULT_STR_PLAYER + DEFAULT_SCORE_PLAYER);
			}
			PlayerPrefs.SetInt (LEADERBOARD_INIT, 1);
		}
	}

	public static bool canBeAddedToLeaderboard (string startedLevel)
	{
		Regex regex = new Regex(@"[0-9]+"); // Pattern to look for
		Match match = regex.Match (startedLevel);
		int levelNumber = int.Parse (match.Value);

		if (levelNumber > 1) {
			return false;
		}

		return true;
	}

	public static string[] getLeaderboard() {
		int total = TOTAL_POSITIONS_LEADERBOARD;
		
		string[] leaderboard = new string[TOTAL_POSITIONS_LEADERBOARD];
		
		for (int i = 1; i <= total; i++) {
			string position = BASE_STR_LEADERBOARD + i.ToString();
			if (PlayerPrefs.HasKey(position)) {
				leaderboard[i - 1] = PlayerPrefs.GetString(position);
			}
		}

		return leaderboard;
	}

	public static int checkUserEnterLeaderboard(string[] leaderboard, int score) {
		int idxPos = TOTAL_POSITIONS_LEADERBOARD;
		for (; idxPos > 0; idxPos--) {
			string playerPosScore = leaderboard[idxPos - 1].Split('_')[1];
			int scorePos = int.Parse(playerPosScore);
			if (score < scorePos) {
				break;
			}
		}
		return idxPos >= 0 ? idxPos : -1 ;
	}

	public static void addUserLeaderboard (string nameUser, int score, int idxPos, string[] leaderboard)
	{
		int playerPosition = idxPos;
		string currentPlayerScore = leaderboard[idxPos], tempPlayerScore;
	
		for (int j = idxPos; j < TOTAL_POSITIONS_LEADERBOARD - 1; j++) {
			tempPlayerScore = leaderboard[j + 1];
			leaderboard[j + 1] = currentPlayerScore;
			currentPlayerScore = tempPlayerScore;
		}

		leaderboard[playerPosition] = nameUser + "_" + score.ToString();

		for (int i = 0; i < TOTAL_POSITIONS_LEADERBOARD; i++) {
			Debug.Log (leaderboard[i]);
		}

		for (int i = playerPosition + 1; i <= TOTAL_POSITIONS_LEADERBOARD; i++) {
			string position = BASE_STR_LEADERBOARD + i.ToString();
			PlayerPrefs.SetString(position, leaderboard[i - 1]);
		}
	}

	public static int getFullScore(int score, float leftTime, int leftLives) {
		int totalScore = score;
		totalScore += ((int)leftTime * SCORE_PER_SECOND);
		totalScore += (leftLives * SCORE_PER_LIVE);
		return totalScore;
	}

	public static int getLivesScore(int lives) {
		return (lives * SCORE_PER_LIVE);
	}

	public static int getTimeScore(float time) {
		return ((int)time * SCORE_PER_SECOND);
	}
}

