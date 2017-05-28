using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Networking;


public class ChaMultiController : NetworkBehaviour {

	public float speed = 5.0F;

	RaycastHit hit;

	float distanceTravelled = 0;

	AudioSource audi;

	public AudioClip jump;

	public AudioClip fall;

	public AudioClip walk;

	public AudioClip run1;



	bool onGround;



	Vector3 lastPosition;



	// Use this for initialization

	void Start () {

		Cursor.lockState = CursorLockMode.Locked;

		lastPosition = transform.position;

		audi = this.gameObject.GetComponent<AudioSource> ();

	}



	// Update is called once per frame

	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKey (KeyCode.LeftShift)) {

			audi.pitch = 1.7f;

			speed = 5.0f;

		} else {

			audi.pitch = 1f;

			speed = 3.0f;

		}



		float translation = Input.GetAxis ("Vertical");

		float straffe = Input.GetAxis ("Horizontal");









		Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider> ().center;



		Vector3 direction = new Vector3 (straffe, 0, translation);

		if (direction.sqrMagnitude > 1) {

			direction.Normalize ();

		}

		direction = direction * speed * Time.deltaTime;



		if (Physics.Raycast (physicsCentre, this.transform.rotation * direction.normalized, out hit, 0.7f) || Physics.Raycast (physicsCentre + new Vector3 (0, -0.5f, 0), this.transform.rotation * direction.normalized, out hit, 0.7f) ||	Physics.Raycast (physicsCentre + new Vector3 (0, 0.5f, 0), this.transform.rotation * direction.normalized, out hit, 0.7f)) {



		} else {

			transform.Translate (direction);

			if ((translation != 0 || straffe != 0) && !audi.isPlaying && onGround) {

				if (speed == 3.0f) {

					audi.clip = walk;

					audi.Play ();

				} else {

					audi.clip = run1;

					audi.Play ();

				} 

			}

		}





		//	Vector3 p1 = this.transform.position + this.GetComponent<CapsuleCollider> ().center + Vector3.up * -this.GetComponent<CapsuleCollider> ().height * 0.5F;

		//	Vector3 p2 = p1 + Vector3.up * this.GetComponent<CapsuleCollider> ().height;

		//	if (Physics.CapsuleCast (p1, p2, this.GetComponent<CapsuleCollider> ().radius, this.transform.rotation * direction.normalized, out hit, 0.06f)) 

		//		Debug.Log (hit.distance + " " + hit.transform.gameObject.name);

		//	else {

		//		transform.Translate (direction);

		//		if ((translation != 0 || straffe != 0) && !audi.isPlaying && onGround) {

		//			if (speed == 3.0f) {

		//				audi.clip = walk;

		//				audi.Play ();

		//			} else {

		//				audi.clip = run1;

		//				audi.Play ();

		//			} 

		//		}

		//	}



		//	Debug.DrawRay (physicsCentre + new Vector3(0,-0.5f,0),this.transform.rotation * direction.normalized * 0.6f, Color.red, 1);

		//	Debug.DrawRay (physicsCentre + new Vector3(0,0.5f,0),this.transform.rotation * direction.normalized * 0.6f, Color.red, 1);

		//	Debug.DrawRay (physicsCentre,this.transform.rotation * direction.normalized * 0.6f, Color.red, 1);

		//	Debug.DrawRay (physicsCentre,Vector3.down * 1.3f, Color.red, 1);

		if (Physics.Raycast (physicsCentre, Vector3.down, out hit, 1.3f)) {

			if (hit.transform.gameObject.tag != "Player" && onGround == false) {

				onGround = true;

				audi.clip = fall;

				audi.Play ();

			}

		} else {

			onGround = false;

		}





		if (Input.GetKeyDown (KeyCode.Space) && onGround) {

			this.GetComponent<Rigidbody> ().AddForce (Vector3.up*230);

			audi.clip = jump;

			audi.Play ();

		}









		//distanceTravelled = Vector3.Distance(transform.position, lastPosition);

		//lastPosition = transform.position;

		//Debug.Log ( this.GetComponent<Rigidbody> ().velocity + "    " + distanceTravelled + "   " + hit.normal + "    " + onGround );

	}

}