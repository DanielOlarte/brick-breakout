using UnityEngine;
using System;
using System.Collections;

public class PrefabBrick : MonoBehaviour {

	public Brick.TypeBrick type;
	private Brick brick;

	// Use this for initialization
	void Start () {
		brick = (Brick)BrickFactory.getBrick(type);
		Debug.Log (brick.getType().ToString());
		Debug.Log (brick.getNeededHits ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		brick.addHits ();
		if (brick.getCurrentHits () == brick.getNeededHits ()) {
			Destroy(gameObject);

			GameObject gameObjectGM = GameObject.Find("GameManager");
			GameManager gameManager = (GameManager) gameObjectGM.GetComponent(typeof(GameManager));
			gameManager.minusBrick();
		}
	}
}
