using System;
using System.Text;
using System.Collections;
using System.Reflection;

public class StringUtils
{
	public static string GetStringValue(Enum value) {
		// Get the type
		Type type = value.GetType();
		
		// Get fieldinfo for this type
		FieldInfo fieldInfo = type.GetField(value.ToString());
		
		// Get the stringvalue attributes
		StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
			typeof(StringValueAttribute), false) as StringValueAttribute[];
		
		// Return the first if there was a match.
		return attribs.Length > 0 ? attribs[0].StringValue : null;
	}

	public static int getLevelBySceneName(string sceneName) {
		string levelStr = "Level";
		int lengthLevel = levelStr.Length;
		string numberLevel = sceneName.Substring (lengthLevel, sceneName.Length - lengthLevel);
		return int.Parse(numberLevel);
	}

	public static string getSpaces(int n) {
		return new String(' ', n);
	}
}


