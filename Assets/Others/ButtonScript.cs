using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour{

	public Transform settingMenu;
	public Transform normalMenu;
	public Transform scoreMenu;

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

	public Text[] nameList = new Text[10];
	public Text[] scoreList = new Text[10];

	public void Exit(){
		Application.Quit();
	}

	public void StartGame()

	{

		SceneManager.LoadScene("basic1");
	}

	public void optionMenu(){
		settingMenu.gameObject.SetActive (true);
		normalMenu.gameObject.SetActive (false);
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
		settingMenu.gameObject.SetActive (false);
		normalMenu.gameObject.SetActive (true);
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
	}

	public void backOption(){
		settingMenu.gameObject.SetActive (false);
		normalMenu.gameObject.SetActive (true);
	}

	public void viewScoreBoard(){
		scoreMenu.gameObject.SetActive (true);
		normalMenu.gameObject.SetActive (false);
		for (int i = 1; i <=10; i++) {
			scoreList [10-i].text = PlayerPrefs.GetInt ("score" + i, 0).ToString ();
			nameList [10-i].text = PlayerPrefs.GetString ("name" + i, "Player");
		}
	}

	public void backScoreBoard(){
		scoreMenu.gameObject.SetActive (false);
		normalMenu.gameObject.SetActive (true);
	}

	public void clearScoreBoard(){

		for (int i = 1; i <=10; i++) {
			PlayerPrefs.SetInt ("score" + i, 0);
			PlayerPrefs.SetString ("name" + i, "Player");
			scoreList [10-i].text = PlayerPrefs.GetInt ("score" + i, 0).ToString ();
			nameList [10-i].text = PlayerPrefs.GetString ("name" + i, "Player");
		}
	}

}
