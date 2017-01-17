using KBEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : KBEMain {

	private GameObject o;
	public float speed = 50;


	private string userName;
	private string userPwd;

	// Use this for initialization
	void Start () 
	{
		MonoBehaviour.print("clientapp::start()");
		installEvents();
		initKBEngine();

	
		if (UserInfo.GetSingleton ().userName == "") {
			//创建账号

			userName = System.Guid.NewGuid().ToString("N");
			userPwd = System.Guid.NewGuid ().ToString ("N").Substring (0,16);

			register(userName,userPwd);

		} else {
			//直接登录

			login (UserInfo.GetSingleton ().userName,UserInfo.GetSingleton ().userPwd);
		}

		o =GameObject.Find ("progress");

		ip = "139.199.189.66";


	}
		
	public override void installEvents()
	{
		
		KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
		KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
		KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
		KBEngine.Event.registerOut("onDisableConnect", this, "onDisableConnect");
		KBEngine.Event.registerOut("onConnectStatus", this, "onConnectStatus");
		KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");


	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Space))
		{
			
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
		//注册失败，账号已经存在。
		if(retcode == 7)
		{
			//createAccount is error(注册账号错误)! err=SERVER_ERR_ACCOUNT_CREATE_FAILED [创建账号失败（已经存在一个相同的账号）。]
			print("createAccount is error(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode));
		}

		if (retcode == 0 ){
			UserInfo.GetSingleton ().userName = userName;
			UserInfo.GetSingleton ().userPwd = userPwd;

			login (UserInfo.GetSingleton ().userName,UserInfo.GetSingleton ().userPwd);

			print("createAccount is successfully!(注册账号成功!)");
		}
	}

	public void onLoginBaseappFailed(System.UInt16 failedcode)
	{
		print("loginBaseapp is failed(登陆网关失败), err=" + KBEngineApp.app.serverErr(failedcode));
	}

	public void onDisableConnect()
	{
		print("onDisableConnect");
	}

	public void onConnectStatus(bool success)
	{
		if(!success)
			print("connect(" + KBEngineApp.app.getInitArgs().ip + ":" + KBEngineApp.app.getInitArgs().port + ") is error! (连接错误)");
		else
			print("connect successfully, please wait...(连接成功，请等候...)");
	}

	public void onLoginSuccessfully(System.UInt64 rndUUID, System.Int32 eid, Account accountEntity)
	{
		print("login is successfully!(登陆成功!)");
	}

	public void onLoginBaseapp()
	{
		print("connect to loginBaseapp, please wait...(连接到网关， 请稍后...)");
	}

	private void register(string userName,string userPwd)
	{
		KBEngine.Event.fireIn("createAccount", userName, userPwd,System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
	}

	private void login(string userName,string userPwd)
	{
		KBEngine.Event.fireIn("login", userName, userPwd,System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
	}
}
