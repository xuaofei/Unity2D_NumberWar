using KBEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginController : KBEMain {

	private GameObject progressObject;
	public float speed = 50;


	private string userName;
	private string userPwd;
	public float progress = 0.0f;
	private bool loadMainScene = false;
	private AsyncOperation loadMainSceneOperation;

	// Use this for initialization

	//定义 WaitAndPrint（）方法  
	IEnumerator WaitAndPrint(float waitTime)  
	{  


		yield return new WaitForSeconds(waitTime);  
		//等待之后执行的动作  
		loadMainSceneOperation = SceneManager.LoadSceneAsync ("main");
		loadMainSceneOperation.allowSceneActivation = false;
		loadMainScene = true;

	}


	IEnumerator Wait(float waitTime)  
	{  
		yield return new WaitForSeconds(waitTime);  

		Main.GetSingleton ().Start ();

	}    


	void Start () 
	{
		Main.GetSingleton ().loginController (this);
		initKBEngine();

		StartCoroutine(Wait(1.0F));
		StartCoroutine(WaitAndPrint(3.0F));

		loadMainScene = false;
		progressObject = GameObject.Find ("progress");
	}



	// Update is called once per frame
	void Update () {
		
		float currentPos = progressObject.GetComponent<RectTransform> ().sizeDelta.x + speed * Time.deltaTime;
		float totalPos = 400.0f;
		float currentProgress = currentPos / totalPos;

		if (loadMainScene)
		{
			progress = 50.0f + loadMainSceneOperation.progress / 2;
		}

		if (currentProgress <= progress) {
			progressObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (currentPos, progressObject.GetComponent<RectTransform> ().sizeDelta.y);
		} else {
			progressObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (progress*totalPos, progressObject.GetComponent<RectTransform> ().sizeDelta.y);
		}

		if (currentProgress > 0.99f) {
			
			loadMainSceneOperation.allowSceneActivation = true;
			Destroy (this);
		}
	}

}