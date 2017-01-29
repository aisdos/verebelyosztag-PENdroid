using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField] private AudioSource music;
	private static bool paused = false;
	private static AudioSource _music;

	void Start() {
		_music = music;
	}

	public static void ReloadScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public static void Pause(bool b) {
		paused = b;
		if (b) {
			_music.Pause ();
		} else {
			_music.UnPause ();
		}
	}
	public static bool Pause() {
		return paused;
	}


}
