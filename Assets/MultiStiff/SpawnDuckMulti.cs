using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnDuckMulti : NetworkBehaviour {



	public GameObject duckPrefab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnDuck(){
		CmdSpwnDuck ();
	}

	[Command]
	void CmdSpwnDuck()
	{
		var Duck = (GameObject)Instantiate(
			duckPrefab,
			transform.position,
			transform.rotation);


		NetworkServer.Spawn(Duck);


	}
}
