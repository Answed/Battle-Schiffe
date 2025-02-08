using Godot;
using System.Collections.Generic;
using System.Linq;
public partial class ShipControll : Node
{
	List<ShipManager> ShipList = new List<ShipManager>();
	private List<ShipType> ShipStorage = new  List<ShipType>();
	private Vector2 CreationPosition = new Vector2(500.0F,500.0F);
	private Vector2 MapGridPosition = new Vector2(100.0F,100.0F);
	private Vector2 MapSize = new Vector2(100.0F,100.0F);

	private int MapWidth = 10;
	public enum ShipType{
		TestShip
	}

	[Export] public PackedScene[] Ships;
	
	public override void _Ready()
	{
		AddShipStorage(ShipType.TestShip);
		StageBeginn();
	}
	public override void _Process(double delta){}
	public void AddShipStorage(ShipType newShip){ShipStorage.Add(newShip);}

	public void StageBeginn(){
		//create map grid
		//get correct CreationPostion / Map Position
		StartShipPlacement();
	}

	public void BoardPlacementFinished(){
		LockShips();
	}
	public void StageEnd(){
		StoreShips();
		ClearBoard();
	}

	private void StartShipPlacement(){
		foreach (var x in ShipStorage)
		{
			CreateShip(x);
			//waiting Ship interaction
		}
		ShipStorage.Clear();
	}

	private void StoreShips(){foreach (ShipManager X in ShipList){AddShipStorage(X.GetShipType());}}
	private void ClearBoard(){foreach (ShipManager X in ShipList){X.DestroyShip();}}
	private void LockShips(){foreach (ShipManager X in ShipList){X.LockingShip();}}
	private void UnlockShips(){foreach (ShipManager X in ShipList){X.UnlockingShip();}}

	//extra functions
	private void CreateShip(ShipType type){
		switch (type)
		{
			case ShipType.TestShip:
				ShipList.Add((ShipManager)Ships[0].Instantiate());
				break;

			default:
				GD.PrintErr("CreateShip Error");
				break;
		}
		AddChild(ShipList[^1]);
		ShipList[^1].Position = CreationPosition;
		ShipList[^1].SetShipType(ShipType.TestShip);
	}
}
