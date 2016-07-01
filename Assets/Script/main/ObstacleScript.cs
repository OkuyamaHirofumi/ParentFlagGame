using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour
{
	public float speed = 1.5f;
	Camera camera;
	Vector3 min;
	Vector3 max;
	Vector3 direction;
	/*左右反転させるための２種類のマテリアル*/
	public Material[] materials;
	// Use this for initialization
	void Start ()
	{
		camera = Camera.main;
		min = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));
		max = camera.ViewportToWorldPoint (new Vector3 (1, 1, camera.nearClipPlane));
		if (transform.position.x < min.x) {
			direction = Vector3.right;
			GetComponent<Renderer> ().material = materials [1];
			transform.rotation = Quaternion.Euler (0, 180, 0);
		} else {
			GetComponent<Renderer> ().material = materials [0];
			direction = Vector3.left;
			transform.rotation = Quaternion.Euler (0, 0, 180);
		}
	}
	// Update is called once per frame
	void Update ()
	{	

		if (!player.pauseFlag) {
			if (player.charge > 0) {
				transform.position += direction * speed * Time.deltaTime + Vector3.down * player.obstacleSpeed * Time.deltaTime;
			} else {				  
				transform.position += direction * speed * Time.deltaTime;
			}
		}
	}


	//画面からでた障害物は削除
	void OnTriggerEnter (Collider other)
	{

		if(other.transform.parent.name == "ObstacleDestroyArea"){
			Destroy (gameObject);
		}
	}
}
