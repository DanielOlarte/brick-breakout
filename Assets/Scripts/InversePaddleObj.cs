using UnityEngine;
using System.Collections;

public class InversePaddleObj : MonoBehaviour {

	public float timeEffect = 10.0f;

	private PaddleController paddleController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable() {
		if (paddleController != null) {
			paddleController.inverseDirection ();	
		}
	}

	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag ("Paddle")) {
			Debug.Log ("CollisionInversePaddle-----");

			renderer.enabled = false;

			StartCoroutine ("startObjectEffect");	
		}
	}

	private IEnumerator startObjectEffect()
	{
		GameObject gameObjectGM = GameObject.Find("Paddle");
		paddleController = (PaddleController) gameObjectGM.GetComponent(typeof(PaddleController));
		paddleController.inverseDirection();

		yield return StartCoroutine("waitSeconds");
	}
	
	private IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(timeEffect);
		paddleController.inverseDirection();
		Destroy (gameObject);
		Debug.Log ("Effect Inverse Done");
	}
}
