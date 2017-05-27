using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public static GameManager Instance {
		get {
			if (_instance == null) {
				_instance = (GameManager) FindObjectOfType(typeof(GameManager));
				 
				if ( FindObjectsOfType(typeof(GameManager)).Length > 1 )
				{
					Debug.LogError("[Singleton] Something went really wrong " +
						" - there should never be more than 1 singleton!" +
						" Reopening the scene might fix it.");
					return _instance;
				}

				if (_instance == null) {	

					GameObject go = new GameObject ("GameManager");
					go.AddComponent <GameManager> ();

					DontDestroyOnLoad (go);
				} else {
					Debug.Log("[Singleton] Using instance already created: " +
						_instance.gameObject.name);
				}



			}

			return _instance;
		}
	}

	public int Score{ get; set; }
	public bool Test{ get; set; }

	void Awake()
	{
		_instance = this;
	}

	public void FireAA(){
		//Debug.Log (Score);
	}

}
