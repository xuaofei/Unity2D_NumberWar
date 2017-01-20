using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawGrid : MonoBehaviour {

	// 行数
	private int rows = 14;
	// 列数
	private int cols = 8;

	LineRenderer[] LineRendererArr;

	void Start () {
		int LineRendererCount = rows + cols + 2;
		LineRendererArr = new LineRenderer[LineRendererCount];

		LineRenderer lineRenderer = null;
		for (int i = 0; i < LineRendererCount; i++) {
			lineRenderer = LineRendererArr[i];
			lineRenderer.startWidth = 0.02f;
			lineRenderer.endWidth = 0.02f;
			lineRenderer.startColor = Color.red;
			lineRenderer.endColor = Color.yellow;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
