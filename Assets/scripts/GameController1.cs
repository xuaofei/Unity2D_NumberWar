using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController1 : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerEnterHandler{


	private string text;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag (PointerEventData eventData){
//		print (eventData.pointerEnter.GetType().FullName);
		if(eventData.pointerEnter.gameObject.transform.FindChild ("Text")){
			text = eventData.pointerEnter.gameObject.transform.FindChild ("Text").gameObject.GetComponent<Text> ().text;
		}
	}

	public void OnBeginDrag (PointerEventData eventData){
	}


	public void OnEndDrag (PointerEventData eventData){
	}


	public void OnPointerEnter (PointerEventData eventData){
		
	}

	void OnGUI()
	{
		// 文本显示
		GUIStyle titleStyle2 = new GUIStyle();  
		titleStyle2.fontSize = 20;  
		titleStyle2.normal.textColor = new Color(46f/256f, 163f/256f, 256f/256f, 256f/256f);
		GUI.Label (new Rect (10,10, 200, 50), text,titleStyle2);
	}
}
