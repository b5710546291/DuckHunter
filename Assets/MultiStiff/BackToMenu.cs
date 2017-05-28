using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class BackToMenu : NetworkBehaviour {
	public GameObject _NetworkManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToMenu(){
		Destroy (_NetworkManager.gameObject);
		Network.Disconnect();
		NetworkManager.Shutdown ();
		SceneManager.LoadScene ("MainMenu");
	}
}
