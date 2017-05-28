using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiManager : NetworkBehaviour {

	public int numberOfPlayer { get; set;}
	float time;
	public Text timeText;
	public GameObject spawner;

	public Text winText;
	public Text loseText;
	public GameObject _canvas;



	// Use this for initialization
	void Start () {
		numberOfPlayer = 0;
		time = 10;
		timeText.text = time.ToString ("F0");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShutDown(){
		Destroy (this.gameObject);
		Network.Disconnect();
		NetworkManager.Shutdown ();
	}

	public void StartTimeer(){
		StartCoroutine (timeReduce());
	}

	IEnumerator timeReduce(){
		yield return new WaitForSeconds(1.0f);
		if (time <= 0) {
			spawner.GetComponent<SpawnDuckMulti> ().SpawnDuck();
		} else {
			time -= 1;
			timeText.text = time.ToString ("F0");
			StartCoroutine (timeReduce ());
		}
	}

	void OnPlayerConnected(NetworkPlayer player){
		Debug.Log ("Player connected " + numberOfPlayer);
	}


	public void Win(){
		Cursor.lockState = CursorLockMode.None;
		_canvas.SetActive (true);
		winText.gameObject.SetActive (true);
	}

	public void Lose(){
		Cursor.lockState = CursorLockMode.None;
		if (_canvas.activeInHierarchy) {
			return;
		}
		_canvas.SetActive (true);
		loseText.gameObject.SetActive (true);
	}

}
