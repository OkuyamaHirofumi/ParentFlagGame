using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurboButton : MonoBehaviour {
	// Use this for initialization
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PushTurbo(){
		GameObject.Find ("player").SendMessage ("StartTurbo");
	}
}
