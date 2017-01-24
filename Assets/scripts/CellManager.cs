using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour {

	public GameObject 	cellPrefab;
	private Cell[,]   	cells;

	// Use this for initialization
	void Start () {
		int cellBudgetWitdh = (int)(Screen.width - GameArgs.minMargin * 2) / GameArgs.cols;
		int cellBudgetHeight = (int)(Screen.height - GameArgs.minMargin * 2) / GameArgs.rows;
		//单个卡牌边长
		int side = cellBudgetWitdh >= cellBudgetHeight ? cellBudgetHeight : cellBudgetWitdh;
		//桌面边距
		int hMargin = ( Screen.width - GameArgs.cols * side ) / 2;
		int vMargin = ( Screen.height - GameArgs.rows * side ) / 2;


		this.gameObject.AddComponent<GridLayoutGroup> ();
		GridLayoutGroup grid = this.gameObject.GetComponent<GridLayoutGroup> ();

		grid.cellSize = new Vector2 (side - 1 ,side - 1);
		grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		grid.constraintCount = GameArgs.cols;
		grid.padding = new RectOffset (hMargin,hMargin,vMargin,vMargin);
		grid.spacing = new Vector2 (1f,1f);

		cells = new Cell[GameArgs.rows,GameArgs.cols];
		for (int i = 0; i < GameArgs.rows; i++){
			for (int j = 0; j < GameArgs.cols; j++){
				GameObject tempObject = Instantiate (cellPrefab);
				Cell cell = tempObject.GetComponent<Cell> ();
				cell.cellPostion.x = j;
				cell.cellPostion.y = i;
				cells [i, j] = cell;


//				tempObject.transform.FindChild ("Text").gameObject.GetComponent<Text> ().text = i.ToString () + "-" + j.ToString ();

//				GameObject tempObject = new GameObject ();
				tempObject.transform.SetParent(transform);
//				tempObject.AddComponent<Image> ();
//
//				Image image = tempObject.GetComponent<Image> ();
//				image.overrideSprite = Resources.Load ("sprites/159-mac-shadow.png") as Sprite;
//				image.color = new Color (0.2f,0.5f,0.8f);
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}




	public void Move(Vector2 begin,Vector2 turning, Vector2 end){
		
	}

	public bool CanMove(Vector2 begin,Vector2 turning, Vector2 end){
		return true;
	}

	public void Move(Cell begin,Cell turning, Cell end){

	}

	public bool CanMove(Cell begin,Cell turning, Cell end){
		return true;
	}

	private Cell getCell (Vector2 postion){
		return null;
	}
}
