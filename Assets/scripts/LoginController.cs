using KBEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : KBEMain {

	private GameObject o;
	public float speed = 50;


	private string userName;
	private string userPwd;
	private float progress = 0.0f;
	private bool loadMainScene = false;
	private AsyncOperation loadMainSceneOperation;

	// Use this for initialization
	void Start () 
	{
		MonoBehaviour.print("clientapp::start()");
		installEvents();
		initKBEngine();

		progress = 0.05f;

		if (UserInfo.GetSingleton ().userName == "") {
			//创建账号

			userName = System.Guid.NewGuid().ToString("N");
			userPwd = System.Guid.NewGuid ().ToString ("N").Substring (0,16);

			register(userName,userPwd);

		} else {
			//直接登录

			login (UserInfo.GetSingleton ().userName,UserInfo.GetSingleton ().userPwd);
		}
			
//		ip = "139.199.189.66";

	}

	public override void installEvents()
	{
		o =GameObject.Find ("progress");
		KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
		KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
		KBEngine.Event.registerOut("onDisableConnect", this, "onDisableConnect");
		KBEngine.Event.registerOut("onConnectStatus", this, "onConnectStatus");
		KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
		KBEngine.Event.registerOut("onLoginBaseappResult", this, "onLoginBaseappResult");
		KBEngine.Event.registerOut("onAccountCreateSuccessed", this, "onAccountCreateSuccessed");
	}

	// Update is called once per frame
	void Update () {
		float currentPos = o.GetComponent<RectTransform> ().sizeDelta.x + speed * Time.deltaTime;
		float totalPos = 400.0f;
		float currentProgress = currentPos / totalPos;

		if (loadMainScene)
		{
			progress = 50.0f + loadMainSceneOperation.progress / 2;
		}

		if (currentProgress <= progress) {
			o.GetComponent<RectTransform> ().sizeDelta = new Vector2 (currentPos, o.GetComponent<RectTransform> ().sizeDelta.y);
		} else {
			o.GetComponent<RectTransform> ().sizeDelta = new Vector2 (progress*totalPos, o.GetComponent<RectTransform> ().sizeDelta.y);
		}

		if (currentProgress > 0.99f) {
			loadMainSceneOperation.allowSceneActivation = true;
			Destroy (this);
		}
	}

	public void onCreateAccountResult(System.UInt16 retcode, byte[] datas)
	{
		progress = 0.1f;
		//注册失败，账号已经存在。
		if(retcode == 7)
		{
			//createAccount is error(注册账号错误)! err=SERVER_ERR_ACCOUNT_CREATE_FAILED [创建账号失败（已经存在一个相同的账号）。]
			print("createAccount is error(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode));
		}

		if (retcode == 0 ){
			UserInfo.GetSingleton ().userName = userName;
			UserInfo.GetSingleton ().userPwd = userPwd;
			UserInfo.write2File ();

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


	private void register(string userName,string userPwd)
	{
		KBEngine.Event.fireIn("createAccount", userName, userPwd,System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
	}

	private void login(string userName,string userPwd)
	{
		KBEngine.Event.fireIn("login", userName, userPwd,System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
	}

	public void onConnectStatus(bool success)
	{
		if(!success)
			print("connect(" + KBEngineApp.app.getInitArgs().ip + ":" + KBEngineApp.app.getInitArgs().port + ") is error! (连接错误)");
		else
			print("connect successfully, please wait...(连接成功，请等候...)");
	}

	public void onLoginBaseapp()
	{
		progress = 0.3f;
		print("connect to loginBaseapp, please wait...(连接到网关， 请稍后...)");
	}

	public void onLoginBaseappResult(bool success)
	{
		progress = 0.4f;
	}

	public void onAccountCreateSuccessed()
	{
		progress = 0.5f;
		loadMainSceneOperation = SceneManager.LoadSceneAsync ("main");
		loadMainSceneOperation.allowSceneActivation = false;
		loadMainScene = true;

		print("角色创建成功 ，准备跳转到主场景！");
	}
}