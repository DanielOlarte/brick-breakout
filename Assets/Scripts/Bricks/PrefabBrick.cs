using UnityEngine;
using System;
using System.Collections;

public class PrefabBrick : MonoBehaviour {

	public Brick.TypeBrick type;
	public GameObject particleHit;
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
		if (collision.gameObject.tag == "Player") {
			brick.addHits ();
			Vector3 t = transform.position;
			t.z = -1;
			Instantiate(particleHit, t, Quaternion.identity);
			if (brick.getCurrentHits () == brick.getNeededHits ()) {

				Brick.ObjectBrick objectBrick = brick.getObject();

				if (objectBrick != Brick.ObjectBrick.NONE) {
					String objectBrickStr = StringUtils.GetStringValue(objectBrick);
					Instantiate(Resources.Load(objectBrickStr), gameObject.transform.position, gameObject.transform.rotation);
				}

				int totalPoints = brick.getPoints()*brick.getCurrentHits();

				GameObject gameObjectGM = GameObject.Find("GameManager");
				GameManager gameManager = (GameManager) gameObjectGM.GetComponent(typeof(GameManager));
				gameManager.addScore(totalPoints);
				gameManager.minusBrick();

				Destroy(gameObject);
			}
		}
	}
}
