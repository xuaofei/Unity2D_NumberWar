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
};

//状态
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

//	public CellPostion(){
//		x = 0;
//		y = 0;
//	}

	public override string ToString(){
		return "x:" + x.ToString () + "  y:" + y.ToString ();
	}

	public static bool equal(CellPostion cell1,CellPostion cell2)
	{
		if((cell1.x == cell2.x) && (cell1.y == cell2.y))
			return true;

		return false;
	}

	public static int moveDirection(CellPostion cell1,CellPostion cell2)
	{
		if (cell2.y == cell1.y) {
			if (cell2.x > cell1.x)
				return (int)Direction.Right;
			else
				return (int)Direction.Left;
		}

		if (cell2.x == cell1.x) {
			if (cell2.y > cell1.y)
				return (int)Direction.Up;
			else
				return (int)Direction.Down;
		}

		return (int)Direction.Error;

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
	private GameObject title;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
