using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	public float moveSpeed;
	public float currentMoveSpeed;
	public float edgeScreenOffset;
	
	public Vector3 moveDirection;

	private bool isStopped;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		currentMoveSpeed = moveSpeed;

		if( Input.GetKey(KeyCode.RightArrow) ) {
			moveDirection = transform.right;
			moveDirection.Normalize();
		} else if( Input.GetKey(KeyCode.LeftArrow) ) {
			moveDirection = -transform.right;
			moveDirection.Normalize();
		} else {
			currentMoveSpeed = 0.0f;
		}

		Vector3 target = moveDirection * currentMoveSpeed + currentPosition;
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );

		enforceBounds();
	}

	private void enforceBounds()
	{
		Vector3 newPosition = transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		float xDist = mainCamera.aspect * mainCamera.orthographicSize * edgeScreenOffset; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
		}

		transform.position = newPosition;
	}
}
