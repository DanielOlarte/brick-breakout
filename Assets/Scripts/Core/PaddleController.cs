using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaddleController : MonoBehaviour {

	public float moveSpeed;
	public float currentMoveSpeed;
	public float edgeScreenOffset;
	
	public Vector3 moveDirection;

	private InputManager inputManager;
	private int directionModifier;
	private bool isStopped;

	private Dictionary<string,GameObject> powerups = new Dictionary<string,GameObject>();

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
	}

	// Use this for initialization
	void Start () {
		inputManager = (InputManager)FindObjectOfType (typeof(InputManager));
		directionModifier = 1;
	}

	// Update is called once per frame
	void Update () {

		inputManager.paddleMovementInput (this, moveSpeed, directionModifier);

		moveBallOnPaddle ();
		enforceBounds();
	}

	private void moveBallOnPaddle()
	{
		BallController ballController = (BallController)FindObjectOfType (typeof(BallController));

		if (ballController != null) {
			if ( ballController.getOnPaddle ()) {
				Vector2 ballPos = ballController.transform.position;
				ballPos.x = transform.position.x;
				ballController.transform.position = ballPos;
			}
		}
	}

	private void enforceBounds()
	{
		Vector3 newPosition = transform.position; 
		Camera mainCamera = tk2dCamera.Instance.ScreenCamera;
		Vector3 cameraPosition = mainCamera.transform.position;

		float xDist = tk2dCamera.Instance.ScreenExtents.xMax * edgeScreenOffset; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
		}

		transform.position = newPosition;
	}

	public void inverseDirection() {
		directionModifier = directionModifier * -1;
	}
}
