using KBEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : KBEMain {

	private GameObject o;
	private bool   run = false;
	public float speed = 50;
	
	// Update is called once per frame
	void Update () {
		

		if (!run){
			o =GameObject.Find ("progress");
//			ip = "139.199.189.66";
			KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
			KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
			KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
			KBEngine.Event.registerOut("onDisableConnect", this, "onDisableConnect");

			run = true;
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			string stringAccount = "yyxaf1";
			string stringPasswd = "yy.p112389";
			KBEngine.Event.fireIn("createAccount", stringAccount, stringPasswd,System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
//			KBEngine.Event.fireIn ("createAccount",stringAccount, stringPasswd);
			//KBEngine.Event.fireIn ("login",stringAccount, stringPasswd);
		}


		if (o.GetComponent<RectTransform> ().sizeDelta.x + speed * Time.deltaTime >= 400f) {
			o.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400f, o.GetComponent<RectTransform> ().sizeDelta.y);
		} else {
			o.GetComponent<RectTransform>().sizeDelta = new Vector2(o.GetComponent<RectTransform> ().sizeDelta.x + speed * Time.deltaTime,o.GetComponent<RectTransform>().sizeDelta.y);
		}
	}

	public void onCreateAccountResult(System.UInt16 retcode, byte[] datas)
	{
		if(retcode != 0)
		{
			print("createAccount is error(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode));
			return;
		}
		print("createAccount is successfully!(注册账号成功!)");
	
	}

	public void onLoginBaseappFailed(System.UInt16 failedcode)
	{
		print("loginBaseapp is failed(登陆网关失败), err=" + KBEngineApp.app.serverErr(failedcode));
	}

	public void onDisableConnect()
	{
		print("onDisableConnect");
	}

	public void onLoginSuccessfully(System.UInt64 rndUUID, System.Int32 eid, Account accountEntity)
	{
		print("login is successfully!(登陆成功!)");
	}
}
