using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckAI : MonoBehaviour {

	public GameObject explode;
	public Animator anim;
	public bool flying;
	float speed;
	float prespeed;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		flying = true;
		speed = Random.Range (0.5f, 3.0f);
		prespeed = speed;
		anim.SetBool ("flying", flying);
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Translate (Vector3.down * Time.deltaTime * speed);
		if (this.transform.position.y <= 0f) {
			Destroy (this.gameObject);
		}
	}

	public void death(){
		Instantiate( explode, transform.position, transform.rotation );
		Destroy (this.gameObject);
	}
}
