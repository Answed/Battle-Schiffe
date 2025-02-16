using Godot;
using System.Collections.Generic;

public partial class ShipControll : Node
{
	List<ShipManager> ShipList = new();
	private List<ShipType> ShipStorage = new();
	private Vector2 CreationPosition = new(500.0F,500.0F);
	private Vector2 MapGridPosition = new(100.0F,100.0F);
	private Vector2 MapSize = new(100.0F,100.0F);

	private int MapWidth = 10;

	private int MapHeight = 10;
	public enum ShipType{
		AircraftCarrier,
		AmphibiousAssault,
		Destroyer,
		Corvette,
		Speedboat,
		TestShip
	}

	[Export] public PackedScene[] Ships;
	
	public override void _Ready()
	{
		AddShipStorage(ShipType.TestShip); // For testing
		StageBeginn();
		PlaceLand(new Vector2I(5,5));
	}
	public override void _Process(double delta){}
	public void AddShipStorage(ShipType newShip) { ShipStorage.Add(newShip); }

	private void StageBeginn(){
		//get correct CreationPostion 

		CreateShipGrid();
		StartShipPlacement();
	}

	private void BoardPlacementFinished(){
		LockShips();
	}
	private void StageEnd(){
		StoreShips();
		ClearBoard();
	}
	public void EnemyTurn(){foreach(ShipManager X in ShipList){X.HideShip();}}
	public void PlayerTurn(){foreach(ShipManager X in ShipList){X.UnhideShip();}}

	private void StartShipPlacement(){
		foreach (var x in ShipStorage)
		{
			CreateShip(x);
			//waiting Ship interaction
		}
		ShipStorage.Clear();
	}

	private void StoreShips(){foreach(ShipManager X in ShipList){AddShipStorage(X.GetShipType());}}
	private void ClearBoard(){foreach(ShipManager X in ShipList){X.DestroyShip();}}
	private void LockShips(){foreach(ShipManager X in ShipList){X.LockingShip();}}
	private void UnlockShips(){foreach(ShipManager X in ShipList){X.UnlockingShip();}}

	//extra functions
	private void CreateShip(ShipType type){
		switch (type)
		{
			case ShipType.Speedboat:
				ShipList.Add((ShipManager)Ships[0].Instantiate());
				break;
			case ShipType.Corvette:
				ShipList.Add((ShipManager)Ships[1].Instantiate());
				break;
			case ShipType.Destroyer:
				ShipList.Add((ShipManager)Ships[2].Instantiate());
				break;
			case ShipType.AmphibiousAssault:
				ShipList.Add((ShipManager)Ships[3].Instantiate());
				break;
			case ShipType.AircraftCarrier:
				ShipList.Add((ShipManager)Ships[4].Instantiate());
				break;
			case ShipType.TestShip:
				ShipList.Add((ShipManager)Ships[5].Instantiate());
				break;
			default:
				GD.PrintErr("CreateShip Error");
				break;
		}
		AddChild(ShipList[^1]);
		ShipList[^1].Position = CreationPosition;
		ShipList[^1].SetShipType(type);
	}
	private void CreateShipGrid(){
		//int[,] map = GetMaop();         
		//get map size 
		//get map placement
		//get tile size

		int[,] map = { 	{1, 0, 1, 0, 1, 0, 1, 0, 1, 0}, //placeholder
						{0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
						{1, 0, 1, 0, 1, 0, 1, 0, 1, 0}, 
						{0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
						{1, 0, 1, 0, 1, 0, 1, 0, 1, 0}, 
						{0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
						{1, 0, 1, 0, 1, 0, 1, 0, 1, 0}, 
						{0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
						{1, 0, 1, 0, 1, 0, 1, 0, 1, 0}, 
						{0, 1, 0, 1, 0, 1, 0, 1, 0, 1} };
		PlaceShipGrid();
		GenerateShipGrid(map);
	}
	private void GenerateShipGrid(int[,] map){
		for (int i = 0; i < MapWidth; i++)
		{
			for (int j = 0; j < MapHeight; j++)
			{
				Vector2I PlacmentVector = new Vector2I(j,i);
				if(map[i,j] == 0){PlaceWater(PlacmentVector);}
				if(map[i,j] == 1){PlaceLand(PlacmentVector);}
			}
		}
	}
	private void PlaceShipGrid(){
		GetNode<TileMapLayer>("ShipGrid").Position = MapGridPosition;
		//set size of tiles
	}
	private void PlaceLand(Vector2I position){GetNode<TileMapLayer>("ShipGrid").SetCell(position,4,new Vector2I(0,0),0);}
	private void PlaceWater(Vector2I position){GetNode<TileMapLayer>("ShipGrid").SetCell(position,5,new Vector2I(0,0),0);}
}
