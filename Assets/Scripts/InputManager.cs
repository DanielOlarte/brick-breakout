using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	private int leftClick = 0;
	private int rightClick = 1;

	public void paddleMovementInput(PaddleController paddle, float paddleMoveSpeed, int direction){
		#if UNITY_ANDROID
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
			Camera camera = cameraObject.camera;
			float   fingerXPosition = camera.ScreenToWorldPoint(Input.GetTouch(0).position).x;
			Vector3 newPos = new Vector3(direction*fingerXPosition,paddle.transform.position.y,paddle.transform.position.z);
			
			paddle.transform.position = Vector3.Lerp(paddle.transform.position, newPos, Time.deltaTime*10);
		}
		#endif

		#if UNITY_STANDALONE
		Vector3 currentPosition = paddle.transform.position;
		paddle.currentMoveSpeed = paddleMoveSpeed;
		/*if( inputManager.paddleRight() ) 
		{
			moveDirection = paddle.transform.right*directionModifier;
			moveDirection.Normalize();
		} else if( inputManager.paddleLeft() ) 
		{
			moveDirection = -paddle.transform.right*directionModifier;
			moveDirection.Normalize();
		} else 
		{
			moveDirection = new Vector3(0.0f,0.0f,0.0f);
			currentMoveSpeed = 0.0f;
		}*/
		GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
		Camera camera = cameraObject.camera;
		float mouseXPosition = camera.ScreenToWorldPoint(Input.mousePosition).x;
		Vector3 target = new Vector3( direction*mouseXPosition,currentPosition.y,currentPosition.z);
		paddle.transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime*10 );
		#endif
	}

	public bool releaseBallInput()
	{
		#if UNITY_ANDROID
		if( Input.touchCount == 1)
		{
			float swipeY = 10*Input.GetTouch(0).deltaPosition.y/Screen.height;
			Debug.Log(swipeY);
			if( swipeY > 0.3f )
			{
				return true;
			}
		}
		#endif
		#if UNITY_STANDALONE
		if( Input.GetMouseButtonDown(leftClick) )
		{
			return true;
		}
		#endif
		return false;
	}

	public bool pauseMenuInput() {
		#if UNITY_STANDALONE
		if (Input.GetKeyDown (KeyCode.Escape)) {
			return true;
		}
		#endif
		return false;
	}
}

