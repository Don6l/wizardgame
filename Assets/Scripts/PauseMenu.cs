using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseUI;
	public GameObject playMusic;
	public GameObject pauseMusic;
	private bool paused = false;

	void Start(){
	
		PauseUI.SetActive (false);

	}

	void Update(){
		playMusic = GameObject.Find ("AudioManager");
		AudioSource audioSourcePlay = playMusic.GetComponent<AudioSource> ();
		pauseMusic = GameObject.Find ("PauseAudio");
		AudioSource audioSourcePause = pauseMusic.GetComponent<AudioSource> ();
		audioSourcePause.Pause ();
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;

		}
	
		if (paused) {
			audioSourcePlay.Pause ();
			audioSourcePause.Play();
			PauseUI.SetActive (true);
			Time.timeScale = 0;

		}

		if (!paused) {
			audioSourcePlay.UnPause ();
			audioSourcePause.Stop();
			PauseUI.SetActive (false);
			Time.timeScale = 1;

		}
	}


	public void Resume(){
		paused = false;
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}


	public void Exit(){
		Application.Quit ();
	}
}
