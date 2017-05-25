using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firelight : MonoBehaviour {
	float maxIn = 2.5f;
	float minIn = 2.0f;
	Light light;


	// Use this for initialization
	void Start () {
		light = this.gameObject.GetComponent<Light> ();
		StartCoroutine (ChangeIntens (0.1f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ChangeIntens(float waitTime){
		light.intensity = Random.Range (minIn,maxIn);
		yield return new WaitForSeconds(waitTime);
		StartCoroutine (ChangeIntens (waitTime));
	}
}
