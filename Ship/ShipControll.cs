using BattleSchiffe.Scripts.BoardManager;
using Godot;
using System.Collections.Generic;

public partial class ShipControll : Node
{
	List<ShipManager> ShipList = new();
	private List<ShipType> ShipStorage = new();
	private Vector2 CreationPosition = new(500.0F,500.0F);
	private Vector2 MapGridPosition = new(100.0F,100.0F);
	private Vector2 MapSize = new(100.0F,100.0F);
	private float ShipScale = 1; //ship width = 200
	private float MapScale = 1;
	private int MapWidth = 5;
	private GameUI gameUI;
	private Map map;
	private int MapHeight = 10;

	private bool PlaceShips = false;
	public enum ShipType{
		AircraftCarrier,
		AmphibiousAssault,
		Destroyer,
		Cruiser,
		Corvette,
		Speedboat,
		TestShip
	}

	[Export] public PackedScene[] Ships;
	
	public override void _Ready()
	{
		gameUI = GetNode<GameUI>("../GameUI");
		map = GetNode<Map>("../Map");
		CreationPosition = gameUI.getSpawnPosition();

		AddShipStorage(ShipType.Speedboat); // For testing
		AddShipStorage(ShipType.Corvette);
		//AddShipStorage(ShipType.Destroyer);
		//AddShipStorage(ShipType.Cruiser);

		StageBeginn();
	}
	public override void _Process(double delta)
	{
		if(PlaceShips)
		{
			if(ShipList.Count == 0)
			{
				CreateShip(ShipStorage[0]);
				ShipStorage.Remove(ShipStorage[0]);
			}
			else
			{
				if(ShipList[^1].IsPlaced())
				{
					CreateShip(ShipStorage[0]);
					ShipStorage.Remove(ShipStorage[0]);
				}
			}
			if(ShipStorage.Count == 0) { 
				PlaceShips = false; 
				//call GetCords fpr list of Vector2I cords
				//send cords to board manager
			}
		}
	}
	public void AddShipStorage(ShipType newShip) { ShipStorage.Add(newShip); }

	private void StageBeginn(){
		CreateShipGrid();
		StartShipPlacement();
	}
	private void BoardPlacementFinished() { LockShips(); }
	private void StageEnd(){
		StoreShips();
		ClearBoard();
	}
	public void EnemyTurn(){foreach(ShipManager X in ShipList){X.HideShip();}}
	public void PlayerTurn(){foreach(ShipManager X in ShipList){X.UnhideShip();}}
	private void StartShipPlacement() { PlaceShips = true; }
	public void ScaleShips(float Scale){
		ShipScale = Scale;
		foreach (ShipManager X in ShipList){X.ScaleShip(ShipScale);}
	}
	private void StoreShips(){foreach(ShipManager X in ShipList){AddShipStorage(X.GetShipType());}}
	private void ClearBoard(){foreach(ShipManager X in ShipList){X.DestroyShip();}}
	private void LockShips(){foreach(ShipManager X in ShipList){X.LockingShip();}}
	private void UnlockShips(){foreach(ShipManager X in ShipList){X.UnlockingShip();}}
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
			case ShipType.Cruiser:
				ShipList.Add((ShipManager)Ships[3].Instantiate());
				break;
			case ShipType.AmphibiousAssault:
				ShipList.Add((ShipManager)Ships[4].Instantiate());
				break;
			case ShipType.AircraftCarrier:
				ShipList.Add((ShipManager)Ships[5].Instantiate());
				break;
			case ShipType.TestShip:
				ShipList.Add((ShipManager)Ships[6].Instantiate());
				break;
			default:
				GD.PrintErr("CreateShip Error");
				break;
		}
		AddChild(ShipList[^1]);
		ShipList[^1].Position = CreationPosition;
		ShipList[^1].SetShipType(type);
		ShipList[^1].ScaleShip(ShipScale);
		ShipList[^1].SetMapScale(MapScale);
	}
	private void CreateShipGrid(){
		//int[,] map = GetMaop();   
			  
		MapGridPosition = gameUI.getGameFieldPosition();
		MapSize = gameUI.getGameFieldSize();
		MapWidth = map.getMapWidth();

		int [,] Board = map.GetMap();

		setShipScaling();
		PlaceShipGrid();
		GenerateShipGrid(Board);
	}
	private void setShipScaling(){
		ShipScale = (MapSize.X / MapWidth) / 200; // 200 is default size of the sprites
	}
	private void GenerateShipGrid(int[,] map){
		for (int i = 0; i < MapWidth; i++)
		{
			for (int j = 0; j < MapWidth; j++)
			{
				Vector2I PlacmentVector = new Vector2I(j,i);
				if(map[i,j] == 0){PlaceWater(PlacmentVector);}
				if(map[i,j] == 1){PlaceLand(PlacmentVector);}
			}
		}
	}
	private void PlaceShipGrid(){
		GetNode<TileMapLayer>("ShipGrid").Position = MapGridPosition;
		MapScale = (MapSize.X / MapWidth) / 16; // 16 is the map tile size
		GetNode<TileMapLayer>("ShipGrid").Scale = new Vector2 (MapScale,MapScale);
	}
	private void PlaceLand(Vector2I position) { GetNode<TileMapLayer>("ShipGrid").SetCell(position,4,new Vector2I(0,0),0); }
	private void PlaceWater(Vector2I position){ GetNode<TileMapLayer>("ShipGrid").SetCell(position,5,new Vector2I(0,0),0); }
}
