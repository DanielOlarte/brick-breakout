using System;
using UnityEngine;

public static class TimerUtils
{
	private static float[] timerLevels = new float[]{
		1.0f, // LEVEL01
		420.0f  // LEVEL02
	};
	
	public static float getTimerByLevel(int level) {
		return timerLevels[level];
	}

	public static string getTimeFromFloat(float time) {
		TimeSpan t = TimeSpan.FromSeconds(time);
		
		return string.Format("{0:D2}:{1:D2}",  
		                              t.Minutes, 
		                              t.Seconds);
	}
}
