using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour {
	public GameObject duck;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnDuck (1f));
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnDuck(float waitTime){
		float a = Random.Range (1f, 10f);
		Instantiate (duck, this.gameObject.transform.position, Quaternion.identity);

		yield return new WaitForSeconds(waitTime);
		StartCoroutine (SpawnDuck (a));
	}

	public void Spawn(){
		Instantiate (duck, this.gameObject.transform.position, Quaternion.identity);
	}
}
