using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallController : MonoBehaviour {

	public float baseSpeed;

	private float minSpeed = 0.5f;
	private int INTERVAL_TIME_INCREASE = 16;
	private float INTERVAL_TIME_INCREASE_F = 15.0f;
	private float TOP_MAX_Y = 5.2f;

	private int tickCount;
	private PaddleController paddle;
	private bool onPaddle = true;
	private Dictionary<string,float> speedModifiers = new Dictionary<string, float>();
	private Dictionary<string,GameObject> powerups = new Dictionary<string, GameObject>();
	private InputManager inputManager;
	private bool hasStarted = false;

	public bool getOnPaddle()
	{
		return onPaddle;
	}

	public void setOnPaddle(bool onPaddle) {
		this.onPaddle = onPaddle;
	}

	private void setSpeedModifier(string key,float value)
	{
		if( !speedModifiers.ContainsKey(key) )
		{
			speedModifiers.Add(key, baseSpeed*value);
		}
		else{
			speedModifiers[key] = 0;
			speedModifiers[key] = baseSpeed*value;
		}
	}

	public void setPowerUp(string key, GameObject go, float value)
	{
		if( !powerups.ContainsKey(key) )
		{
			powerups.Add(key, go);
		}
		else{
			Destroy (powerups[key]);
			powerups[key] = go;
		}

		setSpeedModifier (key, value);
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

		setSpeedModifier(ScoreUtils.TIME_MODIFIER, 0.0f);
	}

	void Update () {
		if (hasStarted) {
			increaseTimerSpeed ();
			checkBoundaries ();

			if( inputManager.releaseBallInput() && onPaddle ) {
				rigidbody2D.velocity = new Vector2(0.0f, baseSpeed);
				onPaddle = false;
			}

			if( Mathf.Abs(rigidbody2D.velocity.y) < minSpeed && !onPaddle)
			{
				Vector2 currentVelocity = rigidbody2D.velocity;
				currentVelocity.y = minSpeed;
				rigidbody2D.velocity = currentVelocity;
			}

			adjustVelocity();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 currentVelocity = rigidbody2D.velocity;
		if ( collision.contacts[0].normal.y > 0.9f && collision.gameObject.name.Equals(NameUtils.GO_PADDLE)) {
			float paddleSize = collision.collider.gameObject.transform.localScale.x / 2;
			float paddleCenterModifier = (transform.position.x - collision.collider.gameObject.transform.position.x) / paddleSize;
			float speedModifier = getModifiedSpeed() * paddleCenterModifier;							
			
			currentVelocity.x = speedModifier;
		}
		rigidbody2D.velocity = currentVelocity;
	}

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
		float tickTimer = (TimeScript.FullTimer - TimeScript.Timer) / INTERVAL_TIME_INCREASE_F;
		if ( tickTimer > tickCount && tickCount < INTERVAL_TIME_INCREASE) {
			setSpeedModifier(ScoreUtils.TIME_MODIFIER, tickCount / INTERVAL_TIME_INCREASE_F);
			tickCount += 1;
		}
	}

	public void adjustVelocity()
	{
		float constant = calculateVelocityConstant ();
		rigidbody2D.velocity = new Vector2 (constant*rigidbody2D.velocity.x, constant*rigidbody2D.velocity.y);
	}

	private float calculateVelocityConstant()
	{
		float speedSum = rigidbody2D.velocity.x + rigidbody2D.velocity.y;
		float constant = 0.0f;
		if (Mathf.Abs(speedSum) > 0.0f) {
			constant = Mathf.Sqrt (Mathf.Pow(getModifiedSpeed(), 2) / ( Mathf.Pow(rigidbody2D.velocity.x, 2) + Mathf.Pow(rigidbody2D.velocity.y, 2) ) );
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

		float yDist = TOP_MAX_Y; 
		float yMax = cameraPosition.y + yDist;
		if ( newPosition.y > yMax ) {
			newPosition.y = TOP_MAX_Y;
			if ( newVelocity.y > 0) {
				newVelocity.y = newVelocity.y * -1;
			}
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
}
