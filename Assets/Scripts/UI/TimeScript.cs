using UnityEngine;
using System;
using System.Collections;

public class TimeScript : MonoBehaviour
{
	static private tk2dTextMesh textMesh;
	static private float timer;
	static private float fullTimer;

	static public float Timer
	{
		get
		{
			return timer;	
		}
		
		set
		{
			timer = value;	
		}
	}

	static public float FullTimer
	{
		get
		{
			return fullTimer;	
		}
		
		set
		{
			fullTimer = value;	
		}
	}

	private bool hasStarted = false;

	void Start () {
		int levelNumber = StringUtils.getLevelBySceneName (PlayerPrefs.GetString (ScoreUtils.LEVEL_USER_INIT));
		timer = TimerUtils.getTimerByLevel(levelNumber - 1);
		fullTimer = timer;
		textMesh = GetComponent<tk2dTextMesh>();
		string answer = TimerUtils.getTimeFromFloat (Timer);
		textMesh.text = TimerUtils.TIME_STR + answer;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasStarted) {
			Timer -= Time.deltaTime;
			string answer = TimerUtils.getTimeFromFloat (Timer);
			textMesh.text = TimerUtils.TIME_STR + answer;
			textMesh.Commit();
		}
	}

	public void setStarted(bool started) {
		this.hasStarted = started;
	}
}
