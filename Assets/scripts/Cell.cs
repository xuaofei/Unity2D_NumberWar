using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//类型
enum CellType
{ 
		Card = 1,
		Stone = 2
};

//角色
enum Role
{ 
	PlayerA = 1,
	PlayerB = 2
};

//状态
enum CellStatus
{ 
	Enable = 1,
	Disable = 2,
	Selected_Begin,
	Selected_End,
	Selected_Left,
	Selected_Right,
	Selected_Up,
	Selected_Down,
};

//方向
enum Direction
{ 
	Error,
	Left,
	Right,
	Up,
	Down
};

public struct CellPostion{
	public int x;
	public int y;

	public CellPostion(int x1,int y1){
		x = x1;
		y = y1;
	}

	public static CellPostion ErrorCellPostion(){
		return new CellPostion (int.MaxValue,int.MaxValue);
	}

	public override string ToString(){
		return "x:" + x.ToString () + "  y:" + y.ToString ();
	}

	public static bool equal(CellPostion cell1,CellPostion cell2)
	{
		if((cell1.x == cell2.x) && (cell1.y == cell2.y))
			return true;

		return false;
	}

	public static int moveDirection(CellPostion cell1,CellPostion cell2){
		if (cell2.x == cell1.x) {
			if (cell2.y > cell1.y)
				return (int)Direction.Up;
			else
				return (int)Direction.Down;
		}

		if (cell2.y == cell1.y) {
			if (cell2.x > cell1.x)
				return (int)Direction.Right;
			else
				return (int)Direction.Left;
		}
	
		return (int)Direction.Error;
	}

	public static bool neighbor(CellPostion cell1,CellPostion cell2){
		if (cell2.x == cell1.x) {
			if (System.Math.Abs(cell2.y - cell1.y) == 1)
				return true;
			else
				return false;
		}

		if (cell2.y == cell1.y) {
			if (System.Math.Abs(cell2.x - cell1.x) == 1)
				return true;
			else
				return false;
		}

		return false;
	}

	public static CellPostion getUp(CellPostion cell){
		CellPostion newCellPostion;
		newCellPostion.x = cell.x;
		newCellPostion.y = cell.y + 1;

		if (newCellPostion.y > GameArgs.rows)
			return ErrorCellPostion ();
		return newCellPostion;
	}

	public static CellPostion getDown(CellPostion cell){
		CellPostion newCellPostion;
		newCellPostion.x = cell.x;
		newCellPostion.y = cell.y - 1;

		if (newCellPostion.y < 1)
			return ErrorCellPostion ();
		return newCellPostion;
	}

	public static CellPostion getLeft(CellPostion cell){
		CellPostion newCellPostion;
		newCellPostion.x = cell.x - 1;
		newCellPostion.y = cell.y;

		if (newCellPostion.x < 1)
			return ErrorCellPostion ();
		return newCellPostion;
	}

	public static CellPostion getRight(CellPostion cell){
		CellPostion newCellPostion;
		newCellPostion.x = cell.x + 1;
		newCellPostion.y = cell.y;

		if (newCellPostion.x > GameArgs.cols)
			return ErrorCellPostion ();
		return newCellPostion;
	}
};

public class Cell : MonoBehaviour {

	public int cellType;

	//所属玩家
	public int playerRole;
	public int cellStatus;
	//战斗力
	public uint combatEffectiveness;
	//位置
	public CellPostion cellPostion;


	private GameObject bgImage;
	private GameObject effectsImage;
	private GameObject text;

	private string title;

	// Use this for initialization
	void Start () {
		bgImage = transform.FindChild ("bgImage").gameObject;
		effectsImage = transform.FindChild ("effectsImage").gameObject;
		text = transform.FindChild ("title").gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (text) {
			text.GetComponent<Text> ().text = title;
		}
	}

	public void setTitle(string title){
		this.title = title;
	}

	public void selected(bool selected){
		if (selected) {
			effectsImage.GetComponent<Image> ().sprite = Resources.Load ("sprites/circle", typeof(Sprite)) as Sprite;
		} else {
//			effectsImage.GetComponent<Image> ().sprite = Resources.Load ("sprites/record", typeof(Sprite)) as Sprite;
			effectsImage.GetComponent<Image> ().sprite = null;
		}
	}
}
