using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmergencyText : MonoBehaviour {
	public Text emergencyText;
	// Use this for initialization
	void Awake(){
		GameObject.Find("player").GetComponent<player>().enabled = false;
		GameObject.Find ("MainCanvas").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("Mother").GetComponent<Image> ().enabled = false;
		GameObject.Find ("OyaFlaText").GetComponent<Text> ().enabled = false;
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ChangeText(string s){

		if(s == "1"){
			emergencyText.text = "椅子を回して\n脱出せよ!!";
		}else{
			emergencyText.text = s;
		}
	}
	public void PlayerScriptOnOff(int onoff){
		if(onoff == 0){
			GameObject.Find("player").GetComponent<player>().enabled = false;
		}else if(onoff == 1){
			GameObject.Find("player").GetComponent<player>().enabled = true;

		}

	}
	public void DestroyText(){
		GameObject et;
		et = GameObject.Find ("EmergencyText");
		if(et != null)
			Destroy (et.gameObject);

	}
}
