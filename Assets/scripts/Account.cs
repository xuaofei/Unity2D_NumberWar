using KBEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Account : KBEngine.Entity  {

	public static Account account = null;

	public override void __init__()
	{

		Debug.Log ("Player 已经创建成功！");
		account = this;
	}



	public void onBaseTestFunction(string s) {
		Debug.Log ("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
//		baseCall ("reqAvatarList");
//		cellCall ("onAccountCell");
	}


	public void onCellTestFunction(string s) {
		Debug.Log ("ddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
		//		baseCall ("reqAvatarList");
//		cellCall ("onAccountCell");
	}


	public override void onEnterWorld(){
	
		Debug.Log ("onEnterWorldonEnterWorldonEnterWorldonEnterWorldonEnterWorld");
	}
}