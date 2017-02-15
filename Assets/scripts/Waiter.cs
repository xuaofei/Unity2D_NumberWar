using KBEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : KBEngine.Entity {

	public override void __init__()
	{

		Debug.Log ("Waiter 已经创建成功！");

		//获取大厅公告
		baseCall ("getHallNotice__Base");
	}

	public void playerEnterRoomSuccessd(){
		Debug.Log ("playerEnterRoomSuccessd");
	}

	public void getHallNoticeResult__Client(List<object> noticeList){
		foreach (var notice in noticeList)
		{
			Debug.Log (notice);
		}
		Debug.Log ("getHallNoticeResult__Client:" + noticeList.ToString());
//		baseCall ("MatchPlayer");
	}

	public void DebugClientFunction(string s){
		Debug.Log (s);
	}
}
