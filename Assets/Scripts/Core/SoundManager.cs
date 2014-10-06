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
		musicGO = GameObject.Find(NameUtils.GO_MUSIC); 
		musicGO.audio.clip = audio; 
		musicGO.audio.Play(); 
	}

	public void volumeDown(float value) {
		musicGO.audio.volume = value;
	}
}

