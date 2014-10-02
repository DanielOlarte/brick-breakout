using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private static SoundManager instance;
	private GameObject musicGO;

	public static SoundManager GetInstance() {
		return instance;
	}
	
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start() {
	
	}

	public void changeAudio(AudioClip audio) {
		musicGO = GameObject.Find("Music"); //Finds the game object called Game Music, if it goes by a different name, change this.
		musicGO.audio.clip = audio; //Replaces the old audio with the new one set in the inspector.
		musicGO.audio.Play(); //Plays the audio.
	}

	public void volumeDown(float value) {
		musicGO.audio.volume = value;
	}
}

