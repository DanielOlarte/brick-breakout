using System;
using UnityEngine;

public static class TimerUtils
{
	private static float[] timerLevels = new float[]{
		360.0f, // LEVEL01
		360.0f,  // LEVEL02
		240.0f, // LEVEL03
		420.0f,  // LEVEL04
		360.0f, // LEVEL05
		420.0f,  // LEVEL06
		420.0f, // LEVEL07
		420.0f,  // LEVEL08
		390.0f, // LEVEL09
		480.0f,  // LEVEL10
		480.0f, // LEVEL11
		420.0f,  // LEVEL12
		240.0f, // LEVEL13
		420.0f,  // LEVEL14
		480.0f, // LEVEL15
		420.0f,  // LEVEL16
		360.0f, // LEVEL17
		540.0f,  // LEVEL18
		420.0f, // LEVEL19
		420.0f  // LEVEL20
	};
	
	public static float getTimerByLevel(int level) {
		return timerLevels[level];
	}

	public static int getNumberOfLevels() {
		return timerLevels.Length;
	}

	public static string getTimeFromFloat(float time) {
		TimeSpan t = TimeSpan.FromSeconds(time);
		
		return string.Format("{0:D2}:{1:D2}",  
		                              t.Minutes, 
		                              t.Seconds);
	}
}
