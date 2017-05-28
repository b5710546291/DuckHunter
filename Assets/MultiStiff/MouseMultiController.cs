using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMultiController : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	float sensitivity;
	float smoothing;
	AudioSource audi;
	bool scope;
	float scopeFactor;
	float usedSensitivity;





	GameObject charactor;
	public GameObject crosshair;
	public GameObject ScopeOverlay;
	public GameObject WeaponCamera;
	public GameObject WeaponHolder;
	public Animator gunAnim;
	public ParticleSystem flash;

	public float scopedFOV = 15f;
	float normalFOV;
	float fireTime = 1.2f;
	bool fireable = true;
	float spreadFactor = 0.001f;

	// Use this for initialization
	void Start () {
		sensitivity = PlayerPrefs.GetFloat("sensitivity", 2.0f); 
		smoothing = PlayerPrefs.GetFloat("smoothing", 2.0f); 
		scopeFactor = PlayerPrefs.GetFloat("scopeFactor", 0.06f); 
		usedSensitivity = sensitivity;
		scope = false;
		charactor = this.transform.parent.gameObject;
		audi = this.gameObject.GetComponent<AudioSource> ();
		gunAnim = this.GetComponentInChildren<Animator> ();
		normalFOV = this.GetComponent<Camera> ().fieldOfView;
	}

	// Update is called once per frame
	void Update () {
		var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));


		md = Vector2.Scale (md, new Vector2 (usedSensitivity * smoothing, usedSensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV;

		mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		charactor.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, charactor.transform.up);

		if (Input.GetKeyDown (KeyCode.Mouse0) && fireable) {
			fireable = false;
			Invoke ("fireCD", fireTime);
			audi.Play ();
			if (!scope) {
				gunAnim.Play ("Shoot");
			}


			Vector3 physicsCentre = this.transform.position;
			RaycastHit hit;
			flash.Play ();
			Debug.DrawRay (physicsCentre, this.transform.forward, Color.red);

			int layermask = ~(1 << 8);

			Vector3 direction = transform.forward;
			direction.x += Random.Range(-spreadFactor, spreadFactor);
			direction.y += Random.Range(-spreadFactor, spreadFactor);
			direction.z += Random.Range(-spreadFactor, spreadFactor);

			if (Physics.Raycast (physicsCentre, direction, out hit,Mathf.Infinity,layermask)) {
				if (hit.transform.tag == "Enemy") {

					string tagc = hit.transform.tag;
					GameObject obj = hit.collider.transform.parent.gameObject;
					int i = 1;
					while (tagc == "Enemy") {
						obj = obj.transform.parent.gameObject;
						tagc = obj.transform.tag;
						i++;
						if (i >= 20)
							break;
					}
					if (tagc == "Duck") {
						DuckMultiAI sc = (DuckMultiAI)obj.GetComponent<DuckMultiAI> ();
						sc.death ();



					} 

				} 
			}


		}	
		if (Input.GetButtonDown ("Fire2")) {
			scope = !scope;
			gunAnim.SetBool ("Scoped", scope);

			if (scope) {
				StartCoroutine (scopeIn ());
			} else
				scopeOut ();
		}

	}

	IEnumerator scopeIn(){
		yield return new WaitForSeconds (0.15f);
		scope = true;
		ScopeOverlay.SetActive (true);
		usedSensitivity = sensitivity * scopeFactor;
		crosshair.SetActive (false);
		WeaponCamera.SetActive (false);
		//WeaponHolder.SetActive(false);
		this.GetComponent<Camera> ().fieldOfView = scopedFOV;
	}

	void scopeOut(){
		ScopeOverlay.SetActive (false);
		usedSensitivity = sensitivity;
		crosshair.SetActive (true);
		WeaponCamera.SetActive (true);
		//WeaponHolder.SetActive(true);
		this.GetComponent<Camera> ().fieldOfView = normalFOV;
	}

	void fireCD(){
		fireable = true;
	}

	public void updateOption(float newSensitivity, float smooth, float scope){
		sensitivity = newSensitivity;
		smoothing = smooth;
		scopeFactor = scope;
	}
}
