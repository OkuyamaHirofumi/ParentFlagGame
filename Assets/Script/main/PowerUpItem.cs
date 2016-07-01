using UnityEngine;
using System.Collections;

public class PowerUpItem : MonoBehaviour {
	public string text;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!player.pauseFlag) {
			transform.position += Vector3.down * player.obstacleSpeed * 1.1f * Time.deltaTime;
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Destroy (this.gameObject);
			PowerUpText put = GameObject.Find ("PowerUpText").GetComponent<PowerUpText> ();
			put.ShowText (text);
		}
		if(other.name == "DestroyArea"){
			Destroy (gameObject);
		}
	}
}
