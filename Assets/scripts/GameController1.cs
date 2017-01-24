using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController1 : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IPointerEnterHandler{
	public CellManager cellManager;


	private Cell[] dragCells;
	private int dragCellCount;
	private int maxDragCellCount;


	private string showText;

	// Use this for initialization
	void Start () {
		maxDragCellCount = GameArgs.rows + GameArgs.cols - 1;
		dragCellCount = 0;
		dragCells = new Cell[maxDragCellCount];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnBeginDrag (PointerEventData eventData){


		Cell cell = eventData.pointerEnter.GetComponent<Cell> ();
		if (cell){
			addDragCell (cell);
		}
	}

	public void OnDrag (PointerEventData eventData){
		Cell cell = eventData.pointerEnter.GetComponent<Cell> ();
		if (cell){
			addDragCell (cell);
		}


//		if (typeof(Cell).IsAssignableFrom(eventData.pointerEnter.GetType()))
//		{
//			
//		}
//		print (eventData.pointerEnter.GetType());

//		print (eventData.pointerEnter.GetType().FullName);
//		if(eventData.pointerEnter.gameObject.transform.FindChild ("Text")){
//			text = eventData.pointerEnter.gameObject.transform.FindChild ("Text").gameObject.GetComponent<Text> ().text;
//		}
	}




	public void OnEndDrag (PointerEventData eventData){
		Cell cell = eventData.pointerEnter.GetComponent<Cell> ();
		if (cell){
			addDragCell (cell);
		}

		cleanDragCell ();
	}


	public void OnPointerEnter (PointerEventData eventData){
		
	}

	void OnGUI()
	{
		// 文本显示
		GUIStyle titleStyle2 = new GUIStyle();  
		titleStyle2.fontSize = 20;  
		titleStyle2.normal.textColor = new Color(46f/256f, 163f/256f, 256f/256f, 256f/256f);
		GUI.Label (new Rect (10,10, 200, 50), showText,titleStyle2);
	}

	private void addDragCell(Cell cell){

		Cell begin = null;
		Cell turning = null;
		Cell end = null;

		Direction fristDirection = Direction.Error;
		Direction secondDirection = Direction.Error;


		if ( dragCellCount >= maxDragCellCount) {
			return;
		}

		if (dragCellCount == 0) {
			dragCells [dragCellCount] = cell;
			dragCellCount++;
			return;
		}

		if (dragCellCount >= 1) {
			if (CellPostion.equal (dragCells [dragCellCount - 1].cellPostion, cell.cellPostion)) {
				return;
			}

			begin = dragCells [0];
		}
			
		if (dragCellCount == 1) {
			dragCells [dragCellCount] = cell;
			dragCellCount++;
			return;
		}

		if (dragCellCount >= 2) {
			fristDirection = (Direction)CellPostion.moveDirection (dragCells [0].cellPostion, dragCells [1].cellPostion);

			if (fristDirection == Direction.Error) {
				cleanDragCell ();
				return;
			}
		}
			


		for (int i = 3; i < dragCellCount; i++) {
			Direction tempDire = (Direction)CellPostion.moveDirection (dragCells [i - 2].cellPostion,dragCells [i - 1].cellPostion);
			if (tempDire == Direction.Error){
				cleanDragCell ();
				return;
			}

			if (tempDire != fristDirection) {
				turning = dragCells [i - 1];
				//第二次的方向
				secondDirection = tempDire;
				break;
			}
		}
	


		//新加入的cell的方向
		Direction newCellDirection = (Direction)CellPostion.moveDirection (dragCells [dragCellCount - 1].cellPostion,cell.cellPostion);
		print ("fristDirection:"+fristDirection.ToString());
		print ("newCellDirection:"+newCellDirection.ToString());

		if (secondDirection != Direction.Error) {
			if (newCellDirection == secondDirection) {
				dragCells [dragCellCount] = cell;
				dragCellCount++;
			}
		} else {
			if (newCellDirection == fristDirection) {
				dragCells [dragCellCount] = cell;
				dragCellCount++;
			}
		}

		end = dragCells[dragCellCount - 1];

		showText = dragCellCount.ToString ();
		showText += "   begin:" + begin.cellPostion.ToString ();
		if(turning)
			showText += "   turning:" + turning.cellPostion.ToString ();

		showText += "   end:" + end.cellPostion.ToString ();


		print ("showText:" + showText);
	}


	private void cleanDragCell() {
		for (int i = 0; i < dragCellCount; i++) {
			dragCells [i] = null;
		}

		dragCellCount = 0;
	}

	private void updateDragCell(){
		//需要计算出开始的cell、转向的cell、结束的cell
		Cell begin = null;
		Cell turning = null;
		Cell end = null;
		begin = dragCells[0];
		for(int i = 0;i < dragCellCount; i++){
			Cell tempCell = dragCells[i];

			if (!turning) {
				if ( !CellPostion.equal (begin.cellPostion, tempCell.cellPostion) ) {
					turning = tempCell;
				}
			}

			if (!end) {
				if ( !CellPostion.equal (turning.cellPostion, tempCell.cellPostion) ) {
					end = tempCell;
				}
			}
		}

		cellManager.CanMove(begin,turning,end);
	}
}
