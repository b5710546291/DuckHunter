using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassicManager : MonoBehaviour {
	float time;
	public Text text;
	public Transform Player;
	public bool isOver{ get; set;}
	public bool isWorthy;

	public GameObject mainUI;
	public GameObject overUI;

	public GameObject sadText;
	public GameObject happyText;
	public Text overScoreText;

	public int killcount{ get; set;}
	public Text killCountText;

	public Text[] nameList = new Text[10];
	public Text[] scoreList = new Text[10];

	int saveTo = -1;

	// Use this for initialization
	void Start () {
		time = 5.0f;
		text.text = time.ToString ("F0");
		killcount = 0;
		updateKillCount ();
		isOver = false;
		isWorthy = false;
		StartCoroutine (timeReduce());
	}

	// Update is called once per frame
	void Update () {
	}

	IEnumerator timeReduce(){
		yield return new WaitForSeconds(1.0f);
		if (time <= 0) {
			gameOver ();
		} else {
			time -= 1;
			text.text = time.ToString ("F0");
			StartCoroutine (timeReduce ());
		}
	}

	public void updateKillCount(){
		killCountText.text = "Kill Count: " + killcount.ToString();
	}

	void gameOver(){
		isOver = true;
		mainUI.SetActive (false);
		overUI.SetActive (true);
		overScoreText.text = "Your Score:" +  killcount.ToString();
		int[] scoreTemp = new int[10];
		string[] nameTemp = new string[10];
		for (int i = 1; i <=10; i++) {
			scoreTemp [i - 1] = PlayerPrefs.GetInt ("score" + i, 0);
			nameTemp [i - 1] = PlayerPrefs.GetString ("name" + i, "Player");
			nameList [10-i].text = nameTemp [i - 1];
			scoreList [10-i].text = scoreTemp [i - 1].ToString ();
		}

		for (int i = 1; i <= 10; i++) {
			if (killcount <= scoreTemp [i - 1]) {
				break;
			} else {
				saveTo = i;
			}
		}

		if (saveTo > -1) {
			isWorthy = true;
			happyText.SetActive (true);
		} else {
			sadText.SetActive (true);
		}


		Player.GetComponent<charactorController> ().enabled = false;
		Player.GetComponentInChildren<mouseCharController> ().enabled = false;
		Cursor.lockState = CursorLockMode.None;




	}


	public void toMainMenu(){
		saveScore ();
		SceneManager.LoadScene("MainMenu");
	}

	public void Replay(){
		saveScore ();
		SceneManager.LoadScene("basic1");
	}

	public void saveScore(){
		if (isWorthy) {
			InputField field = happyText.gameObject.transform.GetChild (0).gameObject.GetComponent<InputField>();;
			string nameSave = "Player";
			if (field.text == "") {
				
			} else {
				nameSave = field.text;
			}

			PlayerPrefs.SetInt ("score"+saveTo,killcount);
			PlayerPrefs.SetString ("name"+saveTo,nameSave);
		}
	}



}
