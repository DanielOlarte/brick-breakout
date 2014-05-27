using UnityEngine;
using System;
using System.Collections;

public class PrefabBrick : MonoBehaviour {

	public Brick.TypeBrick type;
	private Brick brick;

	// Use this for initialization
	void Start () {
		brick = (Brick)BrickFactory.getBrick(type);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		brick.addHits ();
		if (brick.getCurrentHits () == brick.getNeededHits ()) {
			Destroy(gameObject);
		}
	}
}
