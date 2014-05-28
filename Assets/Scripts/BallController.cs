using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public Vector2 speed = new Vector2(0.5f,-1f);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(speed * Time.deltaTime);
		checkBoundaries ();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log (collision);
		Vector2 normal = collision.contacts [0].normal;
		if ( Mathf.Approximately( Mathf.Abs(normal.x), 1.0f) ){
			speed.x = Mathf.Abs(speed.x) * normal.x;
		}
		if ( Mathf.Approximately( Mathf.Abs(normal.y) , 1.0f)) {
			speed.y = speed.y * -1;
		}
	}

	void checkBoundaries()
	{
		Vector3 newPosition = transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		Vector3 colliderSize = gameObject.renderer.bounds.extents;
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float xMax = cameraPosition.x + xDist - colliderSize.x;
		float xMin = cameraPosition.x - xDist + colliderSize.x;

		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
			speed.x = speed.x * -1;
			Debug.Log("X Boundary");
		}

		float yDist = mainCamera.orthographicSize; 

		float yMax = cameraPosition.y + yDist - colliderSize.y;

		if ( newPosition.y > yMax ) {
			newPosition.y = Mathf.Clamp( newPosition.y, float.MinValue, yMax );
			speed.y = speed.y * -1;
			Debug.Log("Y Boundary");
		}

		transform.position = newPosition;
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
		GameObject gameObjectGM = GameObject.Find("GameManager");
		GameManager gameManager = (GameManager) gameObjectGM.GetComponent(typeof(GameManager));
		gameManager.updateLivesAndInstantiate ();
	}
}
