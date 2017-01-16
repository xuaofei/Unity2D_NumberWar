using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : KBEMain {

	private GameObject o;
	public float speed = 5;

	// Use this for initialization
	void Start () {
		o =GameObject.Find ("progress");


//		o.GetComponent<RectTransform>().sizeDelta = new Vector2(200F,o.GetComponent<RectTransform>().sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (o.GetComponent<RectTransform> ().sizeDelta.x + speed * Time.deltaTime >= 400f) {
			o.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400f, o.GetComponent<RectTransform> ().sizeDelta.y);
		} else {
			o.GetComponent<RectTransform>().sizeDelta = new Vector2(o.GetComponent<RectTransform> ().sizeDelta.x + speed * Time.deltaTime,o.GetComponent<RectTransform>().sizeDelta.y);
		}
	}
}
