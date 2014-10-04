using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallController : MonoBehaviour {

	public float baseSpeed = 3.0f;
	private int tickCount;
	public float redirectionMaxValue = 0.5f;
	private PaddleController paddle;
	private bool onPaddle = true;
	private Dictionary<string,float> speedModifiers = new Dictionary<string,float>();
	private Dictionary<string,GameObject> powerups = new Dictionary<string,GameObject>();
	private InputManager inputManager;
	private bool hasStarted = false;

	public bool getOnPaddle()
	{
		return onPaddle;
	}

	public void setOnPaddle(bool onPaddle) {
		this.onPaddle = onPaddle;
	}

	public void setSpeedModifier(string key,float value)
	{
		float modifiedSpeed = getModifiedSpeed();
		if( !speedModifiers.ContainsKey(key) )
		{
			speedModifiers.Add(key, modifiedSpeed*value);
		}
		else{
			speedModifiers[key] = 0;
			modifiedSpeed = getModifiedSpeed();
			speedModifiers[key] = modifiedSpeed*value;
		}
	}

	public void setPowerUp(string key,GameObject go)
	{
		if( !powerups.ContainsKey(key) )
		{
			powerups.Add(key, go);
		}
		else{
			Destroy (powerups[key]);
			powerups[key] = go;
		}
	}
				
	public void removePowerUp(string key)
	{
		powerups.Remove (key);
		speedModifiers.Remove (key);
	}
	

	void Start () {
		inputManager = (InputManager)FindObjectOfType (typeof(InputManager));
		tickCount = 1;
		paddle = (PaddleController)FindObjectOfType (typeof(PaddleController));
		Vector3 pos = paddle.gameObject.transform.position;
		pos.y = pos.y + paddle.gameObject.transform.localScale.y / 4;
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasStarted) {
			increaseTimerSpeed ();
			checkBoundaries ();

			if( inputManager.releaseBallInput() && onPaddle ) {
				rigidbody2D.velocity = new Vector2(0.0f,3.0f);
				onPaddle = false;
			}
			//---------para que no se quede atascada horizontalmente--------------
			if( Mathf.Abs(rigidbody2D.velocity.y) < 0.5f && !onPaddle)
			{
				//Debug.Log ("Vel: " + rigidbody2D.velocity.y);
				Vector2 currentVelocity = rigidbody2D.velocity;
				currentVelocity.y = 0.5f;
				rigidbody2D.velocity = currentVelocity;
			}
			//------------------------END-----------------------------------------
			adjustVelocity();
		}
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

	/*void OnBecameInvisible() {
		GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
		if (gameManager != null) {
			gameManager.updateLivesAndInstantiate (gameObject);
		}
		
		Destroy (gameObject);
	}*/
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
		float tickTimer = (TimeScript.FullTimer - TimeScript.Timer) / 15.0f;
		if ( tickTimer > tickCount && tickCount < 16) {
			setSpeedModifier("time_modifier",tickCount / 15.0f);
			tickCount += 1;
		}
	}

	public void adjustVelocity()
	{
		float constant = calculateVelocityConstant ();
		rigidbody2D.velocity = new Vector2 (constant*rigidbody2D.velocity.x,constant*rigidbody2D.velocity.y);
		//Debug.Log ("AdjustVel: " + rigidbody2D.velocity.y);
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
		Camera mainCamera = tk2dCamera.Instance.ScreenCamera;
		Vector3 cameraPosition = mainCamera.transform.position;

		Vector3 colliderSize = gameObject.renderer.bounds.extents;

		float xDistX =  tk2dCamera.Instance.ScreenExtents.xMax; 
		float xMax = cameraPosition.x + xDistX - colliderSize.x;
		float xMin = cameraPosition.x - xDistX + colliderSize.x;

		Vector2 newVelocity = rigidbody2D.velocity;

		if ( newPosition.x < xMin ) {
			newPosition.x = xMin;
			if ( newVelocity.x < 0) {
				newVelocity.x = newVelocity.x * -1;
			}

		}

		if ( newPosition.x > xMax ) {
			newPosition.x = xMax;
			if ( newVelocity.x > 0) {
				newVelocity.x = newVelocity.x * -1;
			}
		}
		float yDist = /*tk2dCamera.Instance.ScreenExtents.yMax*/5.2f; 
		float yMax = cameraPosition.y + yDist/* - colliderSize.y*/;
		if ( newPosition.y > yMax ) {
			newPosition.y = /*Mathf.Clamp( newPosition.y, -yMax, yMax )*/5.2f;
			//Debug.Log ("Velocity Got Max There " + newVelocity.y);
			if ( newVelocity.y > 0) {
				newVelocity.y = newVelocity.y * -1;
			}
			//Debug.Log ("Got Max There " + newVelocity.y);
		}

		if (newPosition.y < -yMax) {
			GameManager gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
			if (gameManager != null) {
				gameManager.updateLivesAndInstantiate (gameObject);
			}
			
			Destroy (gameObject);
		}

		rigidbody2D.velocity = newVelocity;

		transform.position = newPosition;
	}

	public void setStarted(bool started) {
		this.hasStarted = started;
	}

	//------------------------------------------------END CUSTOM FUNCTIONS--------------------------------------------------------
}
