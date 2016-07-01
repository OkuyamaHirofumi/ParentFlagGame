using UnityEngine;
using System.Collections;

public class PlayerStateController : MonoBehaviour {
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GameObject.Find ("player").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Idle(){
		animator.SetTrigger ("IDLE");
		animator.SetBool ("ESCAPE", false);
	}
}
