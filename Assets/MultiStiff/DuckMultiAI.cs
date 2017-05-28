using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DuckMultiAI : NetworkBehaviour {

	public GameObject explode;
	public Animator anim;
	public bool flying;
	float speed;
	float prespeed;
	float curAngle;
	RaycastHit hit;
	string turn;
	public GameObject deadSFX;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		flying = true;
		speed = Random.Range (0.5f, 8.0f);
		prespeed = speed;
		curAngle = this.transform.rotation.eulerAngles.y;
		anim.SetBool ("flying", flying);
		turn = "none";
	}

	// Update is called once per frame
	void Update () {
		move ();
	}

	[Server]
	public void move(){
		if(Time.timeScale == 0)return;
		Vector3 physicsCentre = this.transform.position + this.GetComponent<BoxCollider>().center;
		//	Debug.DrawRay (physicsCentre,Quaternion.AngleAxis(curAngle + 0f, Vector3.up) * Vector3.forward * 4f, Color.red, 1);
		//	Debug.DrawRay (physicsCentre,Quaternion.AngleAxis(curAngle + 45f,this.transform.rotation * Vector3.up) * Vector3.forward * 2.5f, Color.red, 1);
		//	Debug.DrawRay (physicsCentre,Quaternion.AngleAxis(curAngle -45f,this.transform.rotation * Vector3.up) * Vector3.forward * 2.5f, Color.red, 1);
		float randfly = Random.Range (0.0f,10.0f);


		if (Physics.Raycast (physicsCentre, this.transform.rotation * Vector3.forward, out hit, 0.7f,~(1 << 9))) {
			//Debug.Log (hit.transform.gameObject.tag);
			this.gameObject.transform.Translate (Vector3.back * Time.deltaTime * speed * 2f);
		}


		if (Physics.Raycast (physicsCentre, this.transform.rotation * Vector3.forward, out hit, 4f,~(1 << 9))) {
			if (randfly <= 5.0f) {
				bool res = checkPath (physicsCentre, -1);
				if (res) {
					res = checkPath (physicsCentre, 1);
					if (res) {
						curAngle += 180f;
					} else {
						curAngle += 30f;
					}
				} else {
					curAngle -= 30f;
				}
			} else {
				bool res = checkPath (physicsCentre, 1);
				if (res) {
					res = checkPath (physicsCentre, -1);
					if (res) {
						curAngle += 180f;
					} else {
						curAngle -= 30f;
					}
				} else {
					curAngle += 30f;
				}
			}
		} else {
			if (turn == "left") {
				curAngle -= 0.4f;
			} else if (turn == "right") {
				curAngle += 0.4f;
			} else {
				if (randfly <= 0.5f) {
					turn = "left";
					Invoke ("resetTurn",1f);
				} else if(randfly <= 1f){
					turn = "right";
					Invoke ("resetTurn",1f);
				}
			}

		}


		//curAngle += 1f;
		curAngle = curAngle % 360;
		if (flying) {
			this.gameObject.transform.Translate (Vector3.forward * Time.deltaTime * speed);
			this.gameObject.transform.Translate (Vector3.down * Time.deltaTime * 0f);
			this.transform.localRotation = Quaternion.AngleAxis (curAngle, Vector3.up);
		}
		if (this.transform.position.y <= 0f) {
			Destroy (this.gameObject);
		}
		anim.SetBool ("flying", flying);
		//	Debug.DrawRay (physicsCentre,Vector3.down * 0.25f, Color.red, 1);
		//	if (Physics.Raycast (physicsCentre, Vector3.down, out hit, 0.25f)) {
		//		if (hit.transform.gameObject.tag != "Enemy" && hit.transform.gameObject.tag != "Duck" && flying == true) {
		//			flying = false;
		//		}
		//	} else {
		//		flying = true;
		//	}

	}

	public void death(){
		CmdSpawnDeadEffect ();
		NetworkServer.Destroy (this.gameObject);
	}

	bool checkPath(Vector3 physicsCentre ,float rotation){
		if (Physics.Raycast (physicsCentre, Quaternion.AngleAxis (curAngle + (45f * rotation), this.transform.rotation * Vector3.up) * Vector3.forward, out hit, 2.5f,~(1 << 9))) {
			return true;
		} 
		return false;
	}


	void resetTurn(){
		turn = "none";
	}

	[Command]
	void CmdSpawnDeadEffect()
	{
		var explodeOb = (GameObject)Instantiate(
			explode,
			transform.position,
			transform.rotation);
		var deadSFXOb = (GameObject)Instantiate(
			deadSFX,
			transform.position,
			transform.rotation);


		NetworkServer.Spawn(explodeOb);
		NetworkServer.Spawn(deadSFXOb);


	}

}
