﻿using UnityEngine;
using System.Collections;

public class Meteo : MonoBehaviour {
	public float speed = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.down * speed *Time.deltaTime;
	}
}