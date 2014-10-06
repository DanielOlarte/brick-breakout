using UnityEngine;
using System.Collections;

public class InversePaddleObj : MonoBehaviour {

	public float timeEffect;
	public float scoreModifier;

	public GameObject particle;

	private bool paddleDidntCapture = false;
	private string modifierStr = ScoreUtils.INVERSE_PADDLE_MODIFIER;
	private GameManager gameManager;
	private PaddleController paddleController;

	void Start () {
		gameManager = (GameManager)FindObjectOfType (typeof(GameManager));
	}

	void Update () {
	
	}

	void OnDisable() {
		if (paddleController != null) {
			paddleController.inverseDirection ();	
		}
	}

	void OnTriggerEnter2D(Collider2D other)  {
		if (other.gameObject.CompareTag (TagUtils.TAG_PADDLE)) {
			GetComponent<AudioSource>().Play();
			paddleDidntCapture = true;
			renderer.enabled = false;
			StartCoroutine ("startObjectEffect");	
		}
	}

	private IEnumerator startObjectEffect()
	{
		paddleController = (PaddleController) GameObject.Find(NameUtils.GO_PADDLE).GetComponent(typeof(PaddleController));
		paddleController.inverseDirection();
		paddleController.setPowerUp (modifierStr, gameObject);

		Vector3 positionBall = paddleController.transform.position;
		positionBall.z = -2;
		Quaternion rotationBall = paddleController.transform.rotation;
		rotationBall.x = 270;
		
		GameObject particleObject = Instantiate(particle, positionBall, 
		                                        Quaternion.Euler (rotationBall.x, rotationBall.y, rotationBall.z)) 
			as GameObject;
		particleObject.transform.parent = paddleController.transform;


		gameManager.setScoreModifier (modifierStr, scoreModifier);

		yield return StartCoroutine("waitSeconds");
	}
	
	private IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(timeEffect);

		paddleController.removePowerUp (modifierStr);
		gameManager.removeScoreModifier (modifierStr);

		Destroy (gameObject);
	}

	void OnBecameInvisible() {
		if (!paddleDidntCapture) {
			Destroy (gameObject);
		}
	}
}
