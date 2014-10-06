using UnityEngine;
using System;
using System.Collections;

public class PrefabBrick : MonoBehaviour {

	public Brick.TypeBrick type;
	public GameObject particleHit;
	private Brick brick;

	void Start () {
		brick = (Brick)BrickFactory.getBrick(type);
	}

	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == TagUtils.TAG_PLAYER) {
			GetComponent<AudioSource>().Play();
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == TagUtils.TAG_PLAYER) {
			brick.addHits ();

			Vector3 positionHit = transform.position;
			positionHit.z = -1;

			Instantiate(particleHit, positionHit, Quaternion.identity);

			if (brick.getCurrentHits () == brick.getNeededHits ()) {

				Brick.ObjectBrick objectBrick = brick.getObject();

				if (objectBrick != Brick.ObjectBrick.NONE) {
					String objectBrickStr = StringUtils.GetStringValue(objectBrick);
					Instantiate(Resources.Load(objectBrickStr), gameObject.transform.position, gameObject.transform.rotation);
				}

				int totalPoints = brick.getPoints()*brick.getCurrentHits();

				GameObject gameObjectGM = GameObject.Find(NameUtils.GO_GAME_MANAGER);
				GameManager gameManager = (GameManager) gameObjectGM.GetComponent(typeof(GameManager));
				gameManager.addScore(totalPoints);
				gameManager.minusBrick();

				Destroy(gameObject);
			}
		}
	}
}
