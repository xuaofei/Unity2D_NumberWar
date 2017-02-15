using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class Player : KBEngine.Entity {

	public override void __init__()
	{

		Debug.Log ("Player 已经创建成功！");

		//获取大厅公告
	}

	public void DebugClientFunction(string s){
		Debug.Log (s);
	}
}