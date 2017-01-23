using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drawGrid : MonoBehaviour {

	private float lineHeight;
	private float lineWidth = 0.5f;

	void Start () {
		int cellBudgetWitdh = (int)(Screen.width - GameArgs.minMargin * 2) / GameArgs.cols;
		int cellBudgetHeight = (int)(Screen.height - GameArgs.minMargin * 2) / GameArgs.rows;

		int side = cellBudgetWitdh >= cellBudgetHeight ? cellBudgetHeight : cellBudgetWitdh;

		int hMargin = ( Screen.width - GameArgs.cols * side ) / 2;
		int vMargin = ( Screen.height - GameArgs.rows * side ) / 2;


		Image image = null;
		lineHeight = Screen.width - 2 * hMargin;
		for (int i = 0; i < GameArgs.rows + 1; i++) {
			GameObject tempObject = new GameObject ();
			tempObject.transform.parent = transform;
			tempObject.AddComponent<Image> ();
			image = tempObject.GetComponent<Image> ();
			image.overrideSprite = Resources.Load ("sprites/159-mac-shadow.png") as Sprite;
			image.color = new Color (1f,0f,0f);

			RectTransform rect = tempObject.GetComponent<RectTransform> ();
			rect.pivot = new Vector2 (0f,0f);
			rect.position = new Vector3 (hMargin,vMargin + i * side,1f);
			rect.sizeDelta = new Vector2 (lineHeight,lineWidth);
		}
			
		lineHeight = Screen.height - 2 * vMargin;
		for (int i = 0; i < GameArgs.cols + 1; i++) {
			GameObject tempObject = new GameObject ();
			tempObject.transform.parent = transform;
			tempObject.AddComponent<Image> ();
			image = tempObject.GetComponent<Image> ();
			image.overrideSprite = Resources.Load ("sprites/159-mac-shadow.png") as Sprite;
			image.color = new Color (1f,0f,0f);

			RectTransform rect = tempObject.GetComponent<RectTransform> ();
			rect.pivot = new Vector2 (0f,0f);
			rect.position = new Vector3 (hMargin + i * side,vMargin,1f);
			rect.sizeDelta = new Vector2 (lineWidth,lineHeight);
		}
	}
}