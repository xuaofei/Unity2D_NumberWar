using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour {

	public GameObject 	cellPrefab;
	private Cell[,]   	cells;

	// Use this for initialization
	void Start () {
		cellPrefab = Resources.Load ("perfabs/cell", typeof(GameObject)) as GameObject;

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

		cells = new Cell[GameArgs.cols,GameArgs.rows];
		for (int i = 0; i < GameArgs.rows; i++){
			for (int j = 0; j < GameArgs.cols; j++){
				GameObject tempObject = Instantiate (cellPrefab);

				Cell cell = tempObject.GetComponent<Cell> ();
				cell.cellPostion.x = j + 1;
				cell.cellPostion.y = GameArgs.rows - i;
				cell.setTitle (cell.cellPostion.ToString());

				cells [j, GameArgs.rows - i - 1] = cell;
				tempObject.transform.SetParent(transform);
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
	//,Cell[] fristSet,Cell[] secondSet
	public bool CanMove(Cell begin,Cell turning, Cell end,Cell[] cells){
		CellPostion beginPos = CellPostion.ErrorCellPostion();
		CellPostion turningPos = CellPostion.ErrorCellPostion();
		CellPostion endPos = CellPostion.ErrorCellPostion();

		Direction fristDirection = Direction.Error;
		Direction secondDirection = Direction.Error;

		if (begin) {
			beginPos = begin.cellPostion;
			begin.selected (true);
		} else {
			return false;
		}

		if (end) {
			endPos = end.cellPostion;
		} else {
			return false;
		}
			
		if (begin && turning && end) {
			turningPos = turning.cellPostion;
			fristDirection = (Direction)CellPostion.moveDirection (beginPos, turningPos);
			secondDirection = (Direction)CellPostion.moveDirection (turningPos, endPos);
		} else if(begin && end){
			fristDirection = (Direction)CellPostion.moveDirection (beginPos, endPos);
		}


		//标记出第一条线的格子
		Cell tempCell = begin;
		CellPostion nextPos = CellPostion.ErrorCellPostion();

		if (turning) {
			//第一条线
			while (tempCell != turning) {
				switch (fristDirection) {
				case Direction.Up:
					nextPos = CellPostion.getUp (tempCell.cellPostion);
					break;
				case Direction.Down:
					nextPos = CellPostion.getDown (tempCell.cellPostion);
					break;
				case Direction.Left:
					nextPos = CellPostion.getLeft (tempCell.cellPostion);
					break;
				case Direction.Right:
					nextPos = CellPostion.getRight (tempCell.cellPostion);
					break;

				default:
					break;
				}

				if (CellPostion.equal (nextPos, CellPostion.ErrorCellPostion ())) {
					return false;
				} else {
					tempCell = getCell (nextPos);
					tempCell.selected (true);
				}
			} 

//			turning.selected (true);


			//第二条线
			tempCell = turning;
			while (tempCell != end) {
				switch (secondDirection) {
				case Direction.Up:
					nextPos = CellPostion.getUp (tempCell.cellPostion);
					break;
				case Direction.Down:
					nextPos = CellPostion.getDown (tempCell.cellPostion);
					break;
				case Direction.Left:
					nextPos = CellPostion.getLeft (tempCell.cellPostion);
					break;
				case Direction.Right:
					nextPos = CellPostion.getRight (tempCell.cellPostion);
					break;

				default:
					break;
				}

				if (CellPostion.equal (nextPos, CellPostion.ErrorCellPostion ())) {
					return false;
				} else {
					tempCell = getCell (nextPos);
					tempCell.selected (true);
				}
			}
		}else {
			tempCell = begin;
			while (tempCell != end) {
				switch (fristDirection) {
				case Direction.Up:
					nextPos = CellPostion.getUp (tempCell.cellPostion);
					break;
				case Direction.Down:
					nextPos = CellPostion.getDown (tempCell.cellPostion);
					break;
				case Direction.Left:
					nextPos = CellPostion.getLeft (tempCell.cellPostion);
					break;
				case Direction.Right:
					nextPos = CellPostion.getRight (tempCell.cellPostion);
					break;

				default:
					break;
				}

				if (CellPostion.equal (nextPos, CellPostion.ErrorCellPostion ())) {
					return false;
				} else {
					tempCell = getCell (nextPos);
					tempCell.selected (true);
				}
			}
		}

//		end.selected (true);
//
//		foreach (Cell cell in cells) {
//			cell.selected (true);
//		}
			

		return true;
	}

	public void cancelSelected(){
		for (int i = 0; i < GameArgs.rows; i++) {
			for (int j = 0; j < GameArgs.cols; j++) {
				cells [j, i].selected (false);
			}
		}
	}

	private Cell getCell (CellPostion postion){
		if (postion.x >= 1 && postion.x <= GameArgs.cols && postion.y >= 1 && postion.y <= GameArgs.rows) {
			return cells [(int)postion.x - 1, (int)postion.y - 1];
		}

		return null;
	}
}
