using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	private int leftClick = 0;

#if UNITY_ANDROID
    private float fingerStartTime  = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
     
    private bool isSwipe = false;
    private float minSwipeDist  = 50.0f;
    private float maxSwipeTime = 1.0f;
#endif

	public void paddleMovementInput(PaddleController paddle, float paddleMoveSpeed, int direction){
		#if UNITY_ANDROID
		if(Input.touchCount == 1)// && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			//GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
			Camera camera = tk2dCamera.Instance.ScreenCamera;
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
		Camera camera = tk2dCamera.Instance.ScreenCamera;
		float mouseXPosition = camera.ScreenToWorldPoint(Input.mousePosition).x;
		Vector3 target = new Vector3( direction*mouseXPosition,currentPosition.y,currentPosition.z);
		paddle.transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime*10 );
		#endif
	}

	public bool releaseBallInput()
	{
		#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    return true;
                                }
                            }

                        }
                        break;
                }
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

