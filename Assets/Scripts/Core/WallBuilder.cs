using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class WallBuilder : MonoBehaviour
{
	public float xOffset;
	public float yOffset;
	private List<List<string>> filedata;
    private Dictionary<string, string> prefabMap;

	// Use this for initialization 24
	void Awake ()
	{
		string levelName = PlayerPrefs.GetString (ScoreUtils.CURRENT_LEVEL_USER);
		readFile("Levels/" + levelName);
        initPrefabMap();
        placeBricks();
	}

	// Update is called once per frame
	void Update ()
	{

	}

    private void initPrefabMap()
    {
        prefabMap = new Dictionary<string, string>();
        prefabMap.Add("F","FastBallsBrickPrefab");
        prefabMap.Add("I", "InverseBrickPrefab");
        prefabMap.Add("M", "MultipleBallsBrickPrefab");
		prefabMap.Add("U", "NonDestroyableBrickPrefab");
		prefabMap.Add("N", "NormalBrickPrefab");
        prefabMap.Add("S", "SlowBallsBrickPrefab");
        prefabMap.Add("X", "XResBrickPrefab");
        prefabMap.Add("XX", "XXResBrickPrefab");
    }

    private void readFile(string filename)
	{
		TextAsset sr = Resources.Load(filename) as TextAsset;
		filedata = sr.text.Split('\n').Select(s=>s.Split(',').ToList()).ToList();
	}

    private void placeBricks()
    {
		float halfWidth = tk2dCamera.Instance.ScreenExtents.xMin + xOffset;
		float tempYOffset = /*tk2dCamera.Instance.ScreenExtents.yMax - yOffset - 0.95f*/4.2f;

        foreach (var data in filedata)
        {
			float tempXOffset = halfWidth + xOffset;
            foreach (var item in data)
			{
				if (tempXOffset < tk2dCamera.Instance.ScreenExtents.xMax - 2*xOffset)
                {
                    instantiateBrick(item,tempXOffset, tempYOffset);
                }
                else
                {
					//Debug.Log ("TempXOffset: " + tempXOffset + " HalfWidth: " + halfWidth + " XOffset: " + (-xOffset));
                    break;
                }
				tempXOffset += xOffset;
            }
            tempYOffset -= yOffset;
        }
    }

    private void instantiateBrick(string type,float xOffset,float yOffset)
    {
		string item = type.Trim ();
		if (prefabMap.ContainsKey (item)) {
			//Debug.Log ("PositionX: " + xOffset);
			GameObject.Instantiate (Resources.Load (prefabMap [item]), new Vector3 (xOffset, yOffset, 0), Quaternion.identity);
		}
    }
}

