using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Invoke ("death", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void death(){
		Destroy (this.gameObject);
	}
}
