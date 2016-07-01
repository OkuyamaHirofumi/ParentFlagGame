using UnityEngine;
using System.Collections;

public class DetectCube : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		BackGroundGenelator.Generate ();
//		GameObject.Find ("TransitionPanel").SendMessage ("TransitionBG", 2);
		if (other.name == "DestroyArea"){
			Destroy (gameObject.transform.parent.gameObject);
		}

		
	}
}
