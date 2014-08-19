using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class WallBuilder : MonoBehaviour
{
	public float xOffset = 0.35f;
	public float yOffset = 0.15f;
	private List<List<string>> filedata;
    private Dictionary<string, string> prefabMap;

	// Use this for initialization 24
	void Awake ()
	{
        readFile("./Assets/" + Application.loadedLevelName + ".lyt");
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
        prefabMap.Add("F","FastBallsBrick");
        prefabMap.Add("I", "InverseBrick");
        prefabMap.Add("M", "MultipleBallsBrick");
        prefabMap.Add("U", "NonDestroyableBrick");
        prefabMap.Add("N", "NormalBrickPrefab");
        prefabMap.Add("S", "SlowBallsBrick");
        prefabMap.Add("X", "XResBrickPrefab");
        prefabMap.Add("XX", "XXResBrickPrefab");
    }

    private void readFile(string filename)
	{
		TextAsset sr = Resources.Load("Level2") as TextAsset;
		filedata = sr.text.Split('\n').Select(s=>s.Split(',').ToList()).ToList();
	}

    private void placeBricks()
    {
        Camera mainCamera = Camera.main;

        float halfWidth = mainCamera.aspect * mainCamera.orthographicSize;
        float tempYOffset = mainCamera.orthographicSize - yOffset - 1.3f;
        foreach (var data in filedata)
        {
            float tempXOffset = -halfWidth + xOffset;
            foreach (var item in data)
            {
                if (tempXOffset < halfWidth - xOffset)
                {
                    instantiateBrick(item,tempXOffset, tempYOffset);
                }
                else
                {
                    break;
                }
                tempXOffset += xOffset;
            }
            tempYOffset -= yOffset;
        }
    }

    private void instantiateBrick(string type,float xOffset,float yOffset)
    {
        if (prefabMap.ContainsKey(type))
        {
            Instantiate(Resources.Load(prefabMap[type]), new Vector3(xOffset, yOffset, 0), Quaternion.identity);
        }
    }
}

