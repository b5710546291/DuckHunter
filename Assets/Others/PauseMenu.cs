using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	public Transform canvas;
	public Transform Player;
	public Transform settingOp;
	public Transform normalOp;

	public Slider volumeSlider;
	public Slider sensitivitySlider;
	public Slider smoothSlider;
	public Slider scopeSlider;

	public Text volumeText;
	public Text senseText;
	public Text smoothText;
	public Text scopeText;

	float volume;
	float sensitivity;
	float smooth;
	float scope;

	public GameObject classicManager;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !classicManager.GetComponent<ClassicManager>().isOver){
			Pause ();
		}
	}

	public void Pause(){

		canvas.gameObject.SetActive (true);
		Time.timeScale = 0;
		Player.GetComponentInChildren<mouseCharController> ().enabled = false;
		AudioListener.volume = 0;
		Cursor.lockState = CursorLockMode.None;
			
	}

	public void UnPause(){
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		Player.GetComponentInChildren<mouseCharController> ().enabled = true;
		AudioListener.volume = PlayerPrefs.GetFloat ("volume",1);
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void toMainMenu(){
		Time.timeScale = 1;
		AudioListener.volume = PlayerPrefs.GetFloat("volume",1);
		SceneManager.LoadScene("MainMenu");
	}

	public void optionMenu(){
		settingOp.gameObject.SetActive (true);
		normalOp.gameObject.SetActive (false);
		volumeSlider.value = PlayerPrefs.GetFloat ("volume",1);
		sensitivitySlider.value = PlayerPrefs.GetFloat ("sensitivity",2.0f);
		smoothSlider.value = PlayerPrefs.GetFloat ("smoothing",2.0f);
		scopeSlider.value = PlayerPrefs.GetFloat ("scopeFactor",0.06f);
		volume = volumeSlider.value;
		sensitivity = sensitivitySlider.value;
		smooth = smoothSlider.value;
		scope = scopeSlider.value;
		updateText ();
	}

	public void updateText(){
		volumeText.text = volume.ToString("F2");
		senseText.text = sensitivity.ToString("F2");
		smoothText.text = smooth.ToString("F2");
		scopeText.text = scope.ToString("F2");
	}

	public void AdjustVolume(float newVolume){
		volume = newVolume;
		updateText ();
	}

	public void AdjustSense(float newSense){
		sensitivity = newSense;
		updateText ();
	}

	public void AdjustSmooth(float newSmooth){
		smooth = newSmooth;
		updateText ();
	}

	public void AdjustScope(float newScope){
		scope = newScope;
		updateText ();
	}

	public void saveOption(){
		PlayerPrefs.SetFloat("volume",volume);
		PlayerPrefs.SetFloat("sensitivity",sensitivity);
		PlayerPrefs.SetFloat("smoothing",smooth);
		PlayerPrefs.SetFloat("scopeFactor",scope);
		settingOp.gameObject.SetActive (false);
		normalOp.gameObject.SetActive (true);
		Player.GetComponentInChildren<mouseCharController> ().updateOption (sensitivity,smooth,scope);
	}

	public void defaultOption(){
		PlayerPrefs.SetFloat("volume",1);
		PlayerPrefs.SetFloat("sensitivity",2.0f);
		PlayerPrefs.SetFloat("smoothing",2.0f);
		PlayerPrefs.SetFloat("scopeFactor",0.06f);
		volumeSlider.value = PlayerPrefs.GetFloat ("volume",1);
		sensitivitySlider.value = PlayerPrefs.GetFloat ("sensitivity",2.0f);
		smoothSlider.value = PlayerPrefs.GetFloat ("smoothing",2.0f);
		scopeSlider.value = PlayerPrefs.GetFloat ("scopeFactor",0.06f);
		volume = volumeSlider.value;
		sensitivity = sensitivitySlider.value;
		smooth = smoothSlider.value;
		scope = scopeSlider.value;
		updateText ();
		Player.GetComponentInChildren<mouseCharController> ().updateOption (sensitivity,smooth,scope);
	}

	public void backOption(){
		settingOp.gameObject.SetActive (false);
		normalOp.gameObject.SetActive (true);
	}
}


