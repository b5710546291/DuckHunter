using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseCharController : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 2.0f;
	public float smoothing = 2.0f;
	AudioSource audi;
	public DuckSpawner spawn1;
	public DuckSpawner spawn2;
	public DuckSpawner spawn3;
	public DuckSpawner spawn4;
	public DuckSpawner spawn5;
	bool scope;
	public float scopeFactor = 0.2f;
	float usedSensitivity = 2.0f;




	int killcount;
	public Text killCountText;

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
		scope = false;
		charactor = this.transform.parent.gameObject;
		audi = this.gameObject.GetComponent<AudioSource> ();
		killcount = 0;
		killCountText.text = "Kill Count: " + killcount.ToString();
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
			GameManager.Instance.Score = killcount;
			GameManager.Instance.FireAA ();
			fireable = false;
			Invoke ("fireCD", fireTime);
			audi.Play ();
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
					while(tagc == "Enemy"){
						obj = obj.transform.parent.gameObject;
						tagc = obj.transform.tag;
						i++;
						if (i >= 20)
							break;
					}
					if (tagc == "Duck") {
						duckAI sc = (duckAI)obj.GetComponent<duckAI> ();
						sc.death ();

						killcount++;
						killCountText.text = "Kill Count: " + killcount.ToString();
						float rando = Random.Range (0.0f, 5.0f);
						if (rando < 1.0f)
							spawn1.Spawn ();
						else if (rando < 2.0f)
							spawn2.Spawn ();
						else if (rando < 3.0f)
							spawn3.Spawn ();
						else if (rando < 4.0f)
							spawn4.Spawn ();
						else if (rando < 5.0f)
							spawn5.Spawn ();
					}

				}

			} 
		}

		if (Input.GetButtonDown ("Fire2")) {
			scope = !scope;
			gunAnim.SetBool ("Scoped", scope);

			if (scope) {
				StartCoroutine( scopeIn ());
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
		this.GetComponent<Camera> ().fieldOfView = scopedFOV;
	}

	void scopeOut(){
		ScopeOverlay.SetActive (false);
		usedSensitivity = sensitivity;
		crosshair.SetActive (true);
		WeaponCamera.SetActive (true);
		this.GetComponent<Camera> ().fieldOfView = normalFOV;
	}

	void fireCD(){
		fireable = true;
	}
}
