using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour{


	public void Exit(){
		Application.Quit();
	}

	public void StartGame()

	{

		SceneManager.LoadScene("basic1");
	}
}
