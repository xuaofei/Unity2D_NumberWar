using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	private GameObject standardSize;

	private Vector3 pos1;
	private Vector3 pos2;

	// Use this for initialization
	void Start () {
		GameObject standardSize = GameObject.Find ("standardSize");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))  
		{  
			pos1 = Input.mousePosition;  
		}  
		if (Input.GetMouseButtonUp(0))  
		{  
			pos2 = Input.mousePosition;  
//			isReady = true;  
		}  
	}
}
