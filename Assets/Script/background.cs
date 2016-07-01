using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {
	float width,height;
	Camera camera;
	Vector3 min;
	Vector3 max;

	public GameObject changeDetectBox;
	// Use this for initialization
	void Start () {
		camera = Camera.main;
		min = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));
		max = camera.ViewportToWorldPoint (new Vector3 (1, 1, camera.nearClipPlane));
		width = max.x - min.x;
		height = max.y - min.x;
		transform.localScale = new Vector3 (width, height * 1.2f, 2);
//		transform.position = new Vector3(0,0,0);
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
