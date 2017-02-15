using KBEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Main  {

	// Use this for initialization
	private static Main _singleton; //(2)
	private LoginController _loginController;

	private string userName;
	private string userPwd;
	private AsyncOperation loadMainSceneOperation;

	private Main()    //(1)
	{

	}

	public static Main GetSingleton () //(3)
	{
		if (_singleton == null) {
			_singleton = new Main ();
		}
		return _singleton;            
	}


	// Use this for initialization
	public void Start () 
	{

	

		installEvents ();

		if (UserInfo.GetSingleton ().userName == "") {
			//创建账号

			userName = System.Guid.NewGuid().ToString("N");
			userPwd = System.Guid.NewGuid ().ToString ("N").Substring (0,16);

			register(userName,userPwd);

		} else {
			//直接登录

			login (UserInfo.GetSingleton ().userName,UserInfo.GetSingleton ().userPwd);
		}
	}

	public void installEvents()
	{
		Debug.Log ("installEvents");
		KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
		KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
		KBEngine.Event.registerOut("onDisableConnect", this, "onDisableConnect");
		KBEngine.Event.registerOut("onConnectStatus", this, "onConnectStatus");
		KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
		KBEngine.Event.registerOut("onLoginBaseappResult", this, "onLoginBaseappResult");
		KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
		KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
	}


	public void loginController(LoginController loginController){
		_loginController = loginController;
	}



	public void onCreateAccountResult(System.UInt16 retcode, byte[] datas)
	{
		_loginController.progress = 0.1f;
		//注册失败，账号已经存在。
		if(retcode == 7)
		{
			//createAccount is error(注册账号错误)! err=SERVER_ERR_ACCOUNT_CREATE_FAILED [创建账号失败（已经存在一个相同的账号）。]
//			print("createAccount is error(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode));
		}

		if (retcode == 0 ){
			UserInfo.GetSingleton ().userName = userName;
			UserInfo.GetSingleton ().userPwd = userPwd;
			UserInfo.write2File ();

			login (UserInfo.GetSingleton ().userName,UserInfo.GetSingleton ().userPwd);

//			print("createAccount is successfully!(注册账号成功!)");
		}
	}

	public void onLoginBaseappFailed(System.UInt16 failedcode)
	{
//		print("loginBaseapp is failed(登陆网关失败), err=" + KBEngineApp.app.serverErr(failedcode));
	}

	public void onDisableConnect()
	{
//		print("onDisableConnect");
	}


	private void register(string userName,string userPwd)
	{
		KBEngine.Event.fireIn("createAccount", userName, userPwd,System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
	}

	private void login(string userName,string userPwd)
	{
		Debug.Log ("login");
		KBEngine.Event.fireIn("login", userName, userPwd,System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
	}

	public void onConnectStatus(bool success)
	{
//		if(!success)
//			print("connect(" + KBEngineApp.app.getInitArgs().ip + ":" + KBEngineApp.app.getInitArgs().port + ") is error! (连接错误)");
//		else
//			print("connect successfully, please wait...(连接成功，请等候...)");
	}

	public void onLoginBaseapp()
	{
		_loginController.progress = 0.3f;
//		print("connect to loginBaseapp, please wait...(连接到网关， 请稍后...)");
	}

	public void onLoginBaseappResult(bool success)
	{
		_loginController.progress = 0.4f;
	}
		

	public void onLoginFailed(System.UInt16 failedcode)
	{
//		if(failedcode == 20)
//		{
//			print("login is failed(登陆失败), err=" + KBEngineApp.app.serverErr(failedcode) + ", " + System.Text.Encoding.ASCII.GetString(KBEngineApp.app.serverdatas()));
//		}
//		else
//		{
//			print("login is failed(登陆失败), err=" + KBEngineApp.app.serverErr(failedcode));
//		}
	}

	public void onEnterWorld(KBEngine.Entity entity)
	{
		//entity为Account
		Debug.Log ("onEnterWorld");
//		print("onEnterWorld");
//
//		entity.baseCall ("reqAvatarList");
//		print("onEnterWorld2");

		if(entity.isPlayer())
			return;
	}
}