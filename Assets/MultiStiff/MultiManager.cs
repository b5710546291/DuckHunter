using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MultiManager : NetworkBehaviour {

	public int numberOfPlayer { get; set;}
	float time;
	public Text timeText;
	public GameObject spawner;


	// Use this for initialization
	void Start () {
		numberOfPlayer = 0;
		time = 10;
		timeText.text = time.ToString ("F0");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[Client]
	public void ShutDown(){
		Destroy (this.gameObject);
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






}
