using UnityEngine;
using System.Collections;

public class ScrollGameOverScript : MonoBehaviour {

	private Transform contentScrollTransform;
	private Vector3 position;

	// Use this for initialization
	void Start () {
		contentScrollTransform = transform.parent;
		position = contentScrollTransform.localPosition;

		Debug.Log (transform.parent.name);
		Debug.Log (transform.parent.localPosition.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onReleaseScroll() {
		Debug.Log ("OnReleaseScroll");
		Debug.Log (contentScrollTransform.localPosition.y);

		float result = contentScrollTransform.localPosition.y / 3.646055f;
		int letter = Mathf.RoundToInt(result);
		char letterChar = (char)(letter + 97);
		Debug.Log ("Result: " + result + "Letter: " + letterChar);

		position.y = 3.646055f * letter - (0.1f*letter);
		Debug.Log ("Position: " + position.y);
		contentScrollTransform.localPosition = position; 
	}
}
