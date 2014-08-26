using UnityEngine;
using System;
using System.Collections;

public class TimeScript : MonoBehaviour
{
	static private tk2dTextMesh textMesh;
	static private float timer;
	
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

	private bool hasStarted = false;

	// Use this for initialization
	void Start () {
		int levelNumber = StringUtils.getLevelBySceneName (Application.loadedLevelName);
		timer = TimerUtils.getTimerByLevel(levelNumber - 1);
		textMesh = GetComponent<tk2dTextMesh>();
		string answer = TimerUtils.getTimeFromFloat (Timer);
		textMesh.text = "Time: " + answer;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasStarted) {
			Timer -= Time.deltaTime;
			string answer = TimerUtils.getTimeFromFloat (Timer);
			textMesh.text = "Time: " + answer;
			textMesh.Commit();
		}
	}

	public void setStarted(bool started) {
		this.hasStarted = started;
	}
}
