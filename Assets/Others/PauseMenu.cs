using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public Transform canvas;
	public Transform Player;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	public void Pause(){

		canvas.gameObject.SetActive (true);
		Time.timeScale = 0;
		Player.GetComponent<charactorController> ().enabled = false;
		Player.GetComponentInChildren<mouseCharController> ().enabled = false;
		AudioListener.volume = 0;
		Cursor.lockState = CursorLockMode.None;
			
	}

	public void UnPause(){
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		Player.GetComponent<charactorController> ().enabled = true;
		Player.GetComponentInChildren<mouseCharController> ().enabled = true;
		AudioListener.volume = 1;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void toMainMenu(){
		Time.timeScale = 1;
		AudioListener.volume = 1;
		SceneManager.LoadScene("MainMenu");
	}
}
