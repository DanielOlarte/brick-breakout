using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {
	
	public bool isPaused;
	public Transform[] childTransforms;
	public AudioClip sound;

	public SummaryScoreMenuController summaryController;

	private bool isShowingSummary;
	private InputManager inputManager;

	// Use this for initialization
	void Start () {
		inputManager = (InputManager)FindObjectOfType (typeof(InputManager));
		disablePauseMenu ();
		isShowingSummary = false;
	}
	
	// Update is called once per frame
	void Update () {
		isShowingSummary = summaryController.isShowingSummary ();
		if (!isShowingSummary) {
			if (inputManager.pauseMenuInput() && !isPaused) {
				enablePauseMenu();
			} else if (inputManager.pauseMenuInput() && isPaused) {
				disablePauseMenu ();
			}
		}
	}

	public void resumeGame() {
		disablePauseMenu ();
	}

	public void exitGame() {
		Application.LoadLevel ("MainMenu");
		SoundManager.GetInstance ().changeAudio (sound);
	}

	private void enablePauseMenu() {
		foreach (Transform t in childTransforms) {
			t.gameObject.SetActive(true);
		}
		isPaused = true;
		Time.timeScale = 0;
	}

	private void disablePauseMenu() {
		foreach (Transform t in childTransforms) {
			t.gameObject.SetActive(false);
		}
		isPaused = false;
		Time.timeScale = 1;
	}
}
