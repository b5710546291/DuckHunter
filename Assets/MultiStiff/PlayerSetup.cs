
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerSetup : NetworkBehaviour	 {

	[SerializeField]
	Behaviour[] componentsToDisable;

	NetworkManager _networkM = NetworkManager.singleton;
	MultiManager _multiM;


	Camera sceneCamera;

	// Use this for initialization
	void Start () {
		_multiM = _networkM.GetComponent<MultiManager>();
		_multiM.numberOfPlayer++;
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
				
			}
		} else {
			if (_multiM.numberOfPlayer > 2) {
				Cursor.lockState = CursorLockMode.None;
				_multiM.ShutDown ();
				SceneManager.LoadScene ("Multi1");
				return;
			}
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (false);
			}
		}
		if (_multiM.numberOfPlayer == 2) {
			_multiM.StartTimeer ();
		}

	}

	void OnDisable (){
		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}
	}



}
