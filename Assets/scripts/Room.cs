using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : KBEngine.Entity {

	public override void __init__()
	{

		Debug.Log ("Room 已经创建成功！");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onTestRoom(string s){
		Debug.LogError (s);
	}
}
