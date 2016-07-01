using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TurboIcon : MonoBehaviour {
	public GameObject[] Icons;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	void ShowIcons(int number){
		for(int i = 0 ; i < number ; i++){
			Icons [i].SetActive (true);
		}
		for(int i = 2 ; i >= number;i--){
			Icons [i].SetActive (false);
		}
	}
}
