using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class PauseButton : MonoBehaviour {
	Blur blur;
	GameObject ButtonController;
	// Use this for initialization
	void Start () {
		ButtonController = GameObject.Find ("ButtonController");
		blur = Camera.main.GetComponent<Blur>();
		blur.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PushPause(){
		blur.enabled = true;
	
		ButtonController.SendMessage("PauseButtons",true);
		ButtonController.SendMessage ("MoveButtons", false);
		GameObject.Find ("CountDown").GetComponent<Text> ().enabled = false;
		player.pauseFlag = true;
	}
	public void PushGo2Start(){
		blur.enabled = false;
		SceneManager.LoadScene ("start");
		ButtonController.SendMessage("PauseButtons",false);
	}
	public void PushRestart(){
		blur.enabled = false;
		ButtonController.SendMessage("PauseButtons",false);
		ButtonController.SendMessage ("MoveButtons", true);
		player.pauseFlag = false;
		GameObject.Find ("CountDown").GetComponent<Text> ().enabled = true;
	}
}
