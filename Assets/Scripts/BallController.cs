using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public float speed = 3;
	public float redirectionMaxValue = 0.5f;
	private PaddleController paddle;
	private bool onPaddle = true;

	public bool getOnPaddle()
	{
		return onPaddle;
	}

	// Use this for initialization
	void Start () {
		paddle = (PaddleController)FindObjectOfType (typeof(PaddleController));

		Vector3 pos = paddle.gameObject.transform.position;
		pos.y = pos.y + 0.25f;
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		checkBoundaries ();
		if( Input.GetKey(KeyCode.Space) && onPaddle ) {
			rigidbody2D.velocity = new Vector2(0.0f,3.0f);
			onPaddle = false;
		}
		//---------para que no se quede atascada horizontalmente--------------
		if( Mathf.Abs(rigidbody2D.velocity.y) < 0.2f && !onPaddle)
		{
			Vector2 currentVelocity = rigidbody2D.velocity;
			currentVelocity.y = 0.2f;
			
			float constant = calculateVelocityConstant (currentVelocity);
			rigidbody2D.velocity = new Vector2 (constant*currentVelocity.x,constant*currentVelocity.y);
		}
		//------------------------END-----------------------------------------
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 currentVelocity = rigidbody2D.velocity;

		if (Mathf.Approximately (collision.contacts[0].normal.y, 1.0f) && collision.gameObject.name.Equals("Paddle")) {						//si choca con la parte de arriba del paddle
			float paddleCenterModifier = Mathf.Abs( (collision.collider.gameObject.transform.position.x - transform.position.x) / 0.4f );	//Se calcula un porcentaje dependiendo del maximo valor 0.4f 
																																			//que corresponderia al borde del paddle.
			float speedModifier = paddle.moveDirection.x * (speed * redirectionMaxValue) * paddleCenterModifier;							//Y se calcula el speedModifier teniendo en cuenta la direccion del paddle,
																																			//la maxima velocidad de la bola y el maximo porcentaje de redireccion
			currentVelocity.x = currentVelocity.x + speedModifier;
		}
		rigidbody2D.velocity = currentVelocity;
	}
	void OnCollisionExit2D(Collision2D collision)
	{
		Vector2 currentVelocity = rigidbody2D.velocity;
		float constant = calculateVelocityConstant (currentVelocity);
		Vector2 newVelocity = new Vector2 (constant*currentVelocity.x,constant*currentVelocity.y);
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
		Destroy (gameObject);
		GameObject gameObjectGM = GameObject.Find("GameManager");
		GameManager gameManager = (GameManager) gameObjectGM.GetComponent(typeof(GameManager));
		gameManager.updateLivesAndInstantiate ();
	}

}
