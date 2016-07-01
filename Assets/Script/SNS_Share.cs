using UnityEngine;
using System.Collections;

public class SNS_Share : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnShare(){
		StartCoroutine (Share ());
	}
	private IEnumerator Share(){
		yield return null;

		string body = player.height.ToString("f2") + "M 飛んで親フラを回避した！！";
		string URL = "http://hogehoge";
		string path = Application.streamingAssetsPath + "/Twitter.png";
		SocialConnector.Share (body, URL, path);
	}
}
