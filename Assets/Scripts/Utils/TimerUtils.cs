using UnityEngine;

public static class TimerUtils
{
	private static float[] timerLevels = new float[]{
		360.0f, // LEVEL01
		300.0f  // LEVEL02
	};
	
	public static float getTimerByLevel(int level) {
		return timerLevels[level];
	}
}
