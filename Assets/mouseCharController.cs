using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCharController : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
	AudioSource audi;
	public DuckSpawner spawn1;
	public DuckSpawner spawn2;
	public DuckSpawner spawn3;
	public DuckSpawner spawn4;
	public DuckSpawner spawn5;


	GameObject charactor;

	// Use this for initialization
	void Start () {
		charactor = this.transform.parent.gameObject;
		audi = this.gameObject.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV;

		mouseLook.y = Mathf.Clamp (mouseLook.y, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		charactor.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, charactor.transform.up);

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			audi.Play ();
			Vector3 physicsCentre = this.transform.position;
			RaycastHit hit;

			Debug.DrawRay (physicsCentre, this.transform.forward, Color.red);

			int layermask = ~(1 << 8);

			if (Physics.Raycast (physicsCentre, this.transform.forward, out hit,Mathf.Infinity,layermask)) {
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

	}
}
