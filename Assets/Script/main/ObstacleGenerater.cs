﻿using UnityEngine;
using System.Collections;

public class ObstacleGenerater : MonoBehaviour
{
	Camera camera;
	Vector3 min;
	Vector3 max;
	float screenHarfY;
	public GameObject[] obstacle;
	int obstacleID;
	float timer = 0;
	int waitingTime = 2;

	void Start ()
	{
		camera = Camera.main;
		min = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));
		max = camera.ViewportToWorldPoint (new Vector3 (1, 1, camera.nearClipPlane));


	}
	// Update is called once per frame
	void Update ()
	{
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
		int random = Random.Range (0, 2);
		if (random == 0) {
			transform.position = new Vector3 (max.x + 5, (max.y - min.y) * 3 / 4, 0);
		} else {
			transform.position = new Vector3 (min.x - 5, Random.Range ((min.y + max.y) / 2, max.y), 0);
		}
		Debug.Log ("random = " + random.ToString ());
		//登った高さによって生成する障害物を
		if (player.height < 30) {
			obstacleID = 0;
		} else if (player.height < 80) {
			obstacleID = Random.Range(0,1);
		} else if (player.height < 150) {
			obstacleID = Random.Range(1,3);
		} else if(player.height < 200){
			obstacleID = 3;
		}else {
			obstacleID = 4;
		}

		if(obstacleID == 1 && random == 1){
			obstacleID = 2;
		}

		Instantiate (obstacle [obstacleID], transform.position, transform.rotation);
	}
}
