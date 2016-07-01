using UnityEngine;
using System.Collections;

public class PWEffectController: MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
	}

	void Play(){
		GetComponent<ParticleSystem> ().Play ();
	}

}
