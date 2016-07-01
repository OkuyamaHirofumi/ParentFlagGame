using UnityEngine;
using System.Collections;

public class MeteoGenerator : MonoBehaviour {
	Camera camera;
	Vector3 min;
	Vector3 max;

	public GameObject Meteo;
	float timer = 0;
	float waitingTime = 1.5f;
	// Use this for initialization
	void Start () {
		camera = Camera.main;
		min = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));
		max = camera.ViewportToWorldPoint (new Vector3 (1, 1, camera.nearClipPlane));
		transform.position = new Vector3 (transform.position.x, max.y + 1.0f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.pauseFlag) {
			timer += Time.deltaTime;
			if (timer > waitingTime) {
				if (player.escapeFlag && player.charge > 0) {
					Generate ();
				}
				timer = 0;
			}
		}
	}

	void Generate ()
	{		
		if (player.height > 200) {
			transform.position = new Vector3 (Random.Range (min.x, max.x), transform.position.y, 0);
			Instantiate (Meteo, transform.position, transform.rotation);
		}
	}
}
