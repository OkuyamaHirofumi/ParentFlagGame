using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Transition : MonoBehaviour {
	Animator animator;
	float width,height;
	Camera camera;
	Vector3 min;
	Vector3 max;
	Vector3 center;
	GameObject bg;
	GameObject tadashi;
	static int bgID;//背景のID
	public GameObject[] background;
	// Use this for initialization
	void Start () {
		camera = Camera.main;
		min = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));
		max = camera.ViewportToWorldPoint (new Vector3 (1, 1, camera.nearClipPlane));
		center = camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, camera.nearClipPlane));
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void TransitionBG(int ID){
		bgID = ID;
		animator.SetBool ("Transition", true);
	}

	public  void ChangeBackGround(){
//		bg = GameObject.Find (bgName);
//		if(!(bg.tag == "background")){
//			return;
//		}

		tadashi = GameObject.Find ("player");
		background[bgID].transform.position = new Vector3(0,0,4);
		tadashi.transform.position = new Vector3(0,0,0);
		tadashi.transform.localScale = new Vector3 (2, 2, 2);

		//前の背景をデストロイ
		Destroy (background [bgID - 1]);

		tadashi.GetComponent<player> ().background = background[bgID];
	}

	public void ExitTransition(){
		animator.SetBool ("Transition", false);
	
	}
	public void EscapeFlagOn(){
		player.escapeFlag = true;
	}

}
