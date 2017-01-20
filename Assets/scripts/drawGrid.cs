using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawGrid : MonoBehaviour {

	// 行数
	private int rows = 12;
	// 列数
	private int cols = 7;

	private float minMargin = 30f;

	LineRenderer[] rowLineRendererArr;
	LineRenderer[] colLineRendererArr;
	Vector3[] rowLeftPostion; 
	Vector3[] rowRightPostion; 

	Vector3[] colUpPostion; 
	Vector3[] colButtonPostion;

	private float lineWidth = 0.01f;

	void Start () {
		rowLineRendererArr = new LineRenderer[rows + 1];
		LineRenderer lineRenderer = null;
		for (int i = 0; i < rows + 1; i++) {
			GameObject tempObject = new GameObject ();
			tempObject.AddComponent<LineRenderer> ();
			lineRenderer = tempObject.GetComponent<LineRenderer> ();

			lineRenderer.startWidth = lineWidth;
			lineRenderer.endWidth = lineWidth;
			lineRenderer.startColor = Color.green;
			lineRenderer.endColor = Color.green;

			rowLineRendererArr[i] = lineRenderer;

			tempObject.transform.parent = transform;
		}

		colLineRendererArr = new LineRenderer[rows + 1];
		lineRenderer = null;
		for (int i = 0; i < rows + 1; i++) {
			GameObject tempObject = new GameObject ();
			tempObject.AddComponent<LineRenderer> ();
			lineRenderer = tempObject.GetComponent<LineRenderer> ();

			lineRenderer.startWidth = lineWidth;
			lineRenderer.endWidth = lineWidth;
			lineRenderer.startColor = Color.green;
			lineRenderer.endColor = Color.green;

			colLineRendererArr[i] = lineRenderer;

			tempObject.transform.parent = transform;
		}

		int cellBudgetWitdh = (int)(Screen.width - minMargin * 2) / cols;
		int cellBudgetHeight = (int)(Screen.height - minMargin * 2) / rows;

		int side = cellBudgetWitdh >= cellBudgetHeight ? cellBudgetHeight : cellBudgetWitdh;

		int hMargin = ( Screen.width - cols * side ) / 2;
		int vMargin = ( Screen.height - rows * side ) / 2;

		rowLeftPostion = new Vector3[rows + 1];
		rowRightPostion = new Vector3[rows + 1];

		colUpPostion = new Vector3[cols + 1];
		colButtonPostion = new Vector3[cols + 1];

		for (int i = 0; i < rows + 1; i++) {
			rowLeftPostion [i] = Camera.main.ScreenToWorldPoint(new Vector3 (hMargin,vMargin + i * side,1f));
			rowRightPostion [i] = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width - hMargin,vMargin + i * side,1f));
		}

		for (int i = 0; i < cols + 1; i++) {
			colUpPostion [i] = Camera.main.ScreenToWorldPoint(new Vector3 (hMargin + i * side,Screen.height - vMargin,1f));
			colButtonPostion [i] = Camera.main.ScreenToWorldPoint(new Vector3 (hMargin + i * side,vMargin,1f));
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < rows + 1; i++) {
			LineRenderer lineRenderer = rowLineRendererArr [i];
			lineRenderer.SetPosition (0, rowLeftPostion[i]);
			lineRenderer.SetPosition (1, rowRightPostion[i]);
		}

		for (int i = 0; i < cols + 1; i++) {
			LineRenderer lineRenderer = colLineRendererArr [i];
			lineRenderer.SetPosition (0, colUpPostion[i]);
			lineRenderer.SetPosition (1, colButtonPostion[i]);
		}
	}
}