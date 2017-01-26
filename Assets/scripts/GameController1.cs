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

	private Cell begin = null;
	private Cell turning = null;
	private Cell end = null;
	private Cell[] fristSet;
	private Cell[] secondSet;

	private Direction fristDirection = Direction.Error;
	private Direction secondDirection = Direction.Error;

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

		cleanDragCell ();

		if (eventData.pointerEnter) {
			Cell cell = eventData.pointerEnter.GetComponent<Cell> ();
			if (cell){
				addDragCell (cell);
			}
		}
	}

	public void OnDrag (PointerEventData eventData){
		if (eventData.pointerEnter) {
			Cell cell = eventData.pointerEnter.GetComponent<Cell> ();
			if (cell){
				addDragCell (cell);
			}
		}
	}




	public void OnEndDrag (PointerEventData eventData){
		if (eventData.pointerEnter) {
			Cell cell = eventData.pointerEnter.GetComponent<Cell> ();
			if (cell){
				addDragCell (cell);
			}
		}
	}


	public void OnPointerEnter (PointerEventData eventData){
		
	}

	void OnGUI()
	{
		showText = dragCellCount.ToString ();
		if(begin){
			showText += " begin:" + begin.cellPostion.ToString ();
		}

		if(turning)
			showText += " turning:" + turning.cellPostion.ToString ();

		if(end){
			showText += " end:" + end.cellPostion.ToString ();
		}

		// 文本显示
		GUIStyle titleStyle2 = new GUIStyle();  
		titleStyle2.fontSize = 15;  
		titleStyle2.normal.textColor = new Color(46f/256f, 163f/256f, 256f/256f, 256f/256f);
		GUI.Label (new Rect (10,10, 200, 50), showText,titleStyle2);



		Cell[] validCell = new Cell[dragCellCount];
		for (int i = 0; i < dragCellCount; i++) {
			validCell [i] = dragCells [i];
		}

		cellManager.CanMove(begin,turning,end,validCell);
	}

	private void addDragCell(Cell cell){

		if ( dragCellCount >= maxDragCellCount) {
			return;
		}

		if (dragCellCount == 0) {
			dragCells [dragCellCount] = cell;
			dragCellCount++;
			return;
		}

		if (dragCellCount >= 1) {
			begin = dragCells [0];

			if (CellPostion.equal (dragCells [dragCellCount - 1].cellPostion, cell.cellPostion)) {
				return;
			}
		}
			
		if (dragCellCount == 1 && CellPostion.neighbor(dragCells [dragCellCount - 1].cellPostion,cell.cellPostion)) {
			dragCells [dragCellCount] = cell;
			dragCellCount++;
			end = dragCells [dragCellCount - 1];
			return;
		}

		if (dragCellCount >= 2) {
			fristDirection = (Direction)CellPostion.moveDirection (dragCells [0].cellPostion, dragCells [1].cellPostion);

			if (fristDirection == Direction.Error) {
				cleanDragCell ();
				return;
			}
		}
			


		for (int i = 3; i <= dragCellCount; i++) {
			Direction tempDire = (Direction)CellPostion.moveDirection (dragCells [i - 2].cellPostion,dragCells [i - 1].cellPostion);
			if (tempDire == Direction.Error){
				cleanDragCell ();
				return;
			}

			if (tempDire != fristDirection) {
				turning = dragCells [i - 2];
				//第二次的方向
				secondDirection = tempDire;
				break;
			}
		}
	


		//新加入的cell的方向
		Direction newCellDirection = (Direction)CellPostion.moveDirection (dragCells [dragCellCount - 1].cellPostion,cell.cellPostion);
//		print ("fristDirection:"+fristDirection.ToString());
//		print ("secondDirection:"+secondDirection.ToString());
//		print ("newCellDirection:"+newCellDirection.ToString());

		if (secondDirection != Direction.Error) {
			if (newCellDirection == secondDirection && CellPostion.neighbor(dragCells [dragCellCount - 1].cellPostion,cell.cellPostion)) {
				dragCells [dragCellCount] = cell;
				dragCellCount++;
			}
		} else {
			if (CellPostion.neighbor (dragCells [dragCellCount - 1].cellPostion, cell.cellPostion)) {
				if (newCellDirection != fristDirection) {
					turning = dragCells [dragCellCount - 1];
					secondDirection = newCellDirection;
				}

				dragCells [dragCellCount] = cell;
				dragCellCount++;
			}
		}

		end = dragCells[dragCellCount - 1];
	}


	private void cleanDragCell() {
		for (int i = 0; i < dragCellCount; i++) {
			dragCells [i] = null;
		}

		dragCellCount = 0;
		begin = null;
		turning = null;
		end = null;
		fristDirection = Direction.Error;
		secondDirection = Direction.Error;

		cellManager.cancelSelected ();
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
	}
}
