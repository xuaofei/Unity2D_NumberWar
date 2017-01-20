using UnityEngine;  
using System.Collections;  
public class Line : MonoBehaviour  
{   
	public Material mat;  
	public Color color = Color.red;  
	public Vector3 pos1;  
	public Vector3 pos2;  
	public bool isReady = false;  

	void Start()  
	{  
		mat.color = color;  
	}  

	void Update()  
	{  
		if (Input.GetMouseButtonDown(0))  
		{  
			pos1 = Input.mousePosition;  
		}  
		if (Input.GetMouseButtonUp(0))  
		{  
			pos2 = Input.mousePosition;  
			isReady = true;  
		}  
	}  

	void OnPostRender()  
	{  
		if (isReady)  
		{  
			GL.PushMatrix();  
			mat.SetPass(0);  
			GL.LoadOrtho();  
			GL.Begin(GL.LINES);  
			GL.Color(color);  
			GL.Vertex3(pos1.x/Screen.width, pos1.y/Screen.height, pos1.z);  
			GL.Vertex3(pos2.x / Screen.width, pos2.y / Screen.height, pos2.z);  
			GL.End();  
			GL.PopMatrix();  
		}  
	}  
}