using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallController : MonoBehaviour {
	public float paddleDirectionForce = 150.0f;
	public float baseSpeed = 3.0f;
	private int tickCount;
	public float redirectionMaxValue = 0.5f;
	private PaddleController paddle;
	private bool onPaddle = true;
	private Dictionary<string,float> speedModifiers = new Dictionary<string,float>();

	//------------------------------------------------GETTER AND SETTERS------------------------------------------------------
	public bool getOnPaddle()
	{
		return onPaddle;
	}
	
	public void setSpeedModifier(string key,float value)
	{
		if( !speedModifiers.ContainsKey(key) )
		{
			speedModifiers.Add(key,value);
		}
		else{
			speedModifiers[key] = value;
		}
	}
	
	public void removeSpeedModifier(string key)
	{
		speedModifiers.Remove (key);
	}
	//------------------------------------------------END OF GETTER AND SETTERS------------------------------------------------------

	//------------------------------------------------UNITY FUNCTIONS--------------------------------------------------------
	// Use this for initialization
	void Start () {
		tickCount = 1;
		paddle = (PaddleController)FindObjectOfType (typeof(PaddleController));
		Vector3 pos = paddle.gameObject.transform.position;
		pos.y = pos.y + 0.25f;
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		increaseTimerSpeed ();
		checkBoundaries ();
		if( Input.GetKey(KeyCode.Space) && onPaddle ) {
			float paddleSpeed = paddle.currentMoveSpeed;
			rigidbody2D.velocity = new Vector2(0.5f*paddleSpeed,3.0f);
			onPaddle = false;
		}
		//---------para que no se quede atascada horizontalmente--------------
		if( Mathf.Abs(rigidbody2D.velocity.y) < 0.2f && !onPaddle)
		{
			Vector2 currentVelocity = rigidbody2D.velocity;
			currentVelocity.y = 0.2f;
			rigidbody2D.velocity = currentVelocity;
		}
		//------------------------END-----------------------------------------
		adjustVelocity();
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 currentVelocity = rigidbody2D.velocity;
		if ( collision.contacts[0].normal.y > 0.9f && collision.gameObject.name.Equals("Paddle")) {
			float paddleSize = collision.collider.gameObject.transform.localScale.x / 2;
			float paddleCenterModifier = (transform.position.x - collision.collider.gameObject.transform.position.x) / paddleSize;
			float speedModifier = getModifiedSpeed() * paddleCenterModifier;							
			
			currentVelocity.x = speedModifier;
		}
		rigidbody2D.velocity = currentVelocity;
	}
	void OnBecameInvisible() {
		Destroy (gameObject);
		GameObject gameObjectGM = GameObject.Find("GameManager");
		GameManager gameManager = (GameManager) gameObjectGM.GetComponent(typeof(GameManager));
		gameManager.updateLivesAndInstantiate ();
	}
	//------------------------------------------------END OF UNITY FUNCTIONS--------------------------------------------------------

	//------------------------------------------------CUSTOM FUNCTIONS--------------------------------------------------------
	public float getModifiedSpeed()
	{
		float modifier = 0.0f;
		
		foreach(KeyValuePair<string, float> entry in speedModifiers)
		{
			modifier += entry.Value;
		}
		return baseSpeed + modifier;
	}

	private void increaseTimerSpeed()
	{
		float tickTimer = TimeScript.Timer / 10;
		if ( tickTimer > tickCount && tickCount < 11) {
			setSpeedModifier("time_modifier",tickCount / 10.0f);
			tickCount += 1;
		}
	}

	public void adjustVelocity()
	{
		float constant = calculateVelocityConstant ();
		rigidbody2D.velocity = new Vector2 (constant*rigidbody2D.velocity.x,constant*rigidbody2D.velocity.y);
	}

	private float calculateVelocityConstant()
	{
		float speedSum = rigidbody2D.velocity.x + rigidbody2D.velocity.y;
		float constant = 0.0f;

		if (Mathf.Abs(speedSum) > 0.0f) 
		{
			constant = Mathf.Sqrt ( Mathf.Pow(getModifiedSpeed(),2) / ( Mathf.Pow(rigidbody2D.velocity.x,2) + Mathf.Pow(rigidbody2D.velocity.y,2) ) );
		}
		return constant;
	}

	private void checkBoundaries()
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
	//------------------------------------------------END CUSTOM FUNCTIONS--------------------------------------------------------
}
