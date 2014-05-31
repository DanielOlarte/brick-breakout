using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public float speed = 3;

	private GameManager gameManager;
	private PaddleController paddle;

	// Use this for initialization
	void Start () {
		Vector2 newVelocity = new Vector2 (1.5f,-1.5f);
		float constant = calculateVelocityConstant (newVelocity);
		newVelocity = new Vector2 (constant*newVelocity.x,constant*newVelocity.y);
		rigidbody2D.velocity = newVelocity;

		paddle = (PaddleController)FindObjectOfType (typeof(PaddleController));
		gameManager = (GameManager) FindObjectOfType(typeof(GameManager));
	}
	
	// Update is called once per frame
	void Update () {
		checkBoundaries ();
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		Debug.Log ( paddle.moveDirection );
		float constant = calculateVelocityConstant (rigidbody2D.velocity);
		Vector2 newVelocity = new Vector2 (constant*rigidbody2D.velocity.x,constant*rigidbody2D.velocity.y);

		if (Mathf.Approximately (collision.contacts [0].normal.y, 1.0f)) {
			newVelocity.x = newVelocity.x + (paddle.moveDirection.x * 0.5f);
		}

		rigidbody2D.velocity = newVelocity;
	}

	float calculateVelocityConstant(Vector2 diminishedVelocity)
	{
		float constant = 0.0f;
		constant = Mathf.Sqrt ( Mathf.Pow(speed,2) / ( Mathf.Pow(diminishedVelocity.x,2) + Mathf.Pow(diminishedVelocity.y,2) ) );
		return constant;
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

		Vector2 newVelocity = rigidbody2D.velocity;

		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
			newVelocity.x = newVelocity.x * -1;
		}

		float yDist = mainCamera.orthographicSize; 

		float yMax = cameraPosition.y + yDist - colliderSize.y;

		if ( newPosition.y > yMax ) {
			newPosition.y = Mathf.Clamp( newPosition.y, float.MinValue, yMax );
			newVelocity.y = newVelocity.y * -1;
		}

		rigidbody2D.velocity = newVelocity;

		transform.position = newPosition;
	}

	void OnBecameInvisible() {
		gameManager.updateLivesAndInstantiate ();
		Destroy (gameObject);
	}

}
