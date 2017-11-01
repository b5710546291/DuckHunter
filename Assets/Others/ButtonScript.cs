using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

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

	public Transform onlineButton;
	public Transform localButton;
	public Transform clearButton;
	public Transform menuButton;

	public List<PlayerLeaderboardEntry> leaderboard;

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
		leaderboard = null;
	}

	public void viewScoreBoard(){
		scoreMenu.gameObject.SetActive (true);
		normalMenu.gameObject.SetActive (false);
		for (int i = 1; i <=10; i++) {
			scoreList [10-i].text = PlayerPrefs.GetInt ("score" + i, 0).ToString ();
			nameList [10-i].text = PlayerPrefs.GetString ("name" + i, "Player");
		}

		clearButton.gameObject.SetActive (true);
		onlineButton.gameObject.SetActive (true);
		localButton.gameObject.SetActive (false);
		menuButton.gameObject.SetActive (true);
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

	public void onlineScoreBoard(){
		
		clearButton.gameObject.SetActive (false);
		onlineButton.gameObject.SetActive (false);
		menuButton.gameObject.SetActive (false);


		PlayFabSettings.TitleId = "B0E0"; 

		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest { CustomId = "Guess", CreateAccount = true};
		PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

	}

	public void localScoreBoard(){
		for (int i = 1; i <=10; i++) {
			scoreList [10-i].text = PlayerPrefs.GetInt ("score" + i, 0).ToString ();
			nameList [10-i].text = PlayerPrefs.GetString ("name" + i, "Player");
		}

		clearButton.gameObject.SetActive (true);
		onlineButton.gameObject.SetActive (true);
		localButton.gameObject.SetActive (false);
		menuButton.gameObject.SetActive (true);
		leaderboard = null;
	}

	private void OnLoginSuccess(LoginResult result)
	{
		TryGetLeaderBoard ();

	}

	private void OnLoginFailure(PlayFabError error)
	{
		for (int i = 1; i <=10; i++) {
			scoreList [10 - i].text = "";
			nameList [10-i].text = "";
		}
		nameList [0].text = error.GenerateErrorReport ();
		localButton.gameObject.SetActive (true);
		menuButton.gameObject.SetActive (true);
	}


	private void TryGetLeaderBoard(){
		PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest{ StatisticName = "score", StartPosition = 0, MaxResultsCount = 10 },
			(GetLeaderboardResult r) => {
				leaderboard = r.Leaderboard;
				int i = 0;
				foreach(PlayerLeaderboardEntry entry in leaderboard){
					scoreList [i].text = entry.StatValue.ToString();
					nameList [i].text = entry.DisplayName;
					i++;
				}
				for( int j = i;j<10;j++){
					scoreList [j].text = "";
					nameList [j].text = "";
				}
				localButton.gameObject.SetActive (true);
				menuButton.gameObject.SetActive (true);
			},
			OnLoginFailure
		);
		
	}

}
