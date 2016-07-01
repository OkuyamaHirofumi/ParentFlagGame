using UnityEngine;
using System.Collections;

public class BackGroundGenelator : MonoBehaviour {
	public static int count;
	public static GameObject prefab;
	public static GameObject parent;
	// Use this for initialization
	void Start () {
		count = 1;
		parent = GameObject.Find ("sky1");
		prefab = (GameObject)Resources.Load ("Prefab/background/sky");
		Debug.Log (prefab.name);
	}
	
	// Update is called once per frame
	void Update () {

	}
	public static void Generate(){
		count += 2;
		Debug.Log (prefab.name + "を生成！　親：" + parent.name);
		GameObject bg = (GameObject)Instantiate (prefab, Vector3.up * (count * -1), parent.transform.rotation);
		bg.transform.parent = parent.transform;
		bg.transform.localPosition = new Vector3(0,count * (-1),0);
		bg.transform.localScale = Vector3.one;

	}
}
