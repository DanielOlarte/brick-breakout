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
	
	// Use this for initialization
	void Start () {
		int levelNumber = StringUtils.getLevelBySceneName (Application.loadedLevelName);
		timer = TimerUtils.getTimerByLevel(levelNumber - 1);
		textMesh = GetComponent<tk2dTextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		Timer -= Time.deltaTime;
		TimeSpan t = TimeSpan.FromSeconds(timer);
		
		string answer = string.Format("{0:D2}:{1:D2}",  
		                              t.Minutes, 
		                              t.Seconds);
		textMesh.text = "Time: " + answer;
		textMesh.Commit();
	}
}
