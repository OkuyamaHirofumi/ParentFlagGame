using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
	public Button right, left, escape, retry, restart, go2start,pause,turbo,share;
	// Use this for initialization
	void Start ()
	{
//		right = GameObject.Find ("RightButton").GetComponent<Button>();
//		left = GameObject.Find ("LeftButton").GetComponent<Button>();
//		escape = GameObject.Find ("EscapeButton").GetComponent<Button>();
//		retry = GameObject.Find ("RetryButton").GetComponent<Button>();
//		go2start = GameObject.Find ("Go2StartBtn").GetComponent<Button>();

	}
	// Update is called once per frame
	void Update ()
	{

	}
	public void TurboButton(bool OnOff){
		if(OnOff){
			turbo.gameObject.SetActive (true);
		}else{
			turbo.gameObject.SetActive (false);
		}
	}
	public  void GameOverStateButtons ()
	{
		right.gameObject.SetActive (false);
		left.gameObject.SetActive (false);
		escape.gameObject.SetActive (false);
		retry.gameObject.SetActive (true);
		share.gameObject.SetActive (true);
		turbo.gameObject.SetActive (false);
	}

	public  void StartStateButtons ()
	{
		right.gameObject.SetActive (false);
		left.gameObject.SetActive (false);
		retry.gameObject.SetActive (false);
		escape.gameObject.SetActive (true);
		share.gameObject.SetActive (false);
		turbo.gameObject.SetActive (false);
	}

	public  void PauseButtons (bool OnOff)
	{
		if (OnOff) {
			restart.gameObject.SetActive (true);
			go2start.gameObject.SetActive (true);
			escape.gameObject.SetActive (false);
			pause.gameObject.SetActive (false);
			turbo.gameObject.SetActive (false);
		} else {
			restart.gameObject.SetActive (false);
			go2start.gameObject.SetActive (false);
			pause.gameObject.SetActive (true);
			if (!player.escapeFlag) {
				escape.gameObject.SetActive (true);
			}
			if(player.turboCount > 0){
				turbo.gameObject.SetActive (true);
			}
		}
	}

	public void MoveButtons (bool OnOff)
	{
		if (OnOff) {
			right.gameObject.SetActive (true);
			left.gameObject.SetActive (true);
		} else {
			right.gameObject.SetActive (false);
			left.gameObject.SetActive (false);
		}
	}
}
