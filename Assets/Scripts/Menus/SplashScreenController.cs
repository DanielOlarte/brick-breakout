using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour {

	public GameObject logoGO;

	public float limitAlpha = 0.05f;

	private float fadeSpeed = 1.0f;
	private bool fading = false;
	
	// Use this for initialization
	void Start () {
		StartCoroutine (startCountdownFade ());
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine(changeBackground());
		if (logoGO && fading) {
			Color tempColor = logoGO.renderer.material.color;
			tempColor.a = Mathf.Lerp(logoGO.renderer.material.color.a, 0, Time.deltaTime * fadeSpeed);
			logoGO.renderer.material.color = tempColor;

			if (logoGO.renderer.material.color.a <= limitAlpha){
				changeBackground();
			}
		}

	}

	IEnumerator startCountdownFade() {
		yield return new WaitForSeconds(2.0f);
		fading = true;
	}

	private void changeBackground() {
		Application.LoadLevel("MainMenu");
	}
}
