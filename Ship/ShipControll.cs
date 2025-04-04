using BattleSchiffe.Scripts.BoardManager;
using Godot;
using System.Collections.Generic;

public partial class ShipControll : Node
{
	private List<ShipManager> _shipList = new();
	private List<ShipType> _shipStorage = new();
	private Vector2 _creationPosition = new(500.0F,500.0F);
	private Vector2 _mapGridPosition = new(100.0F,100.0F);
	private Vector2 _mapSize = new(100.0F,100.0F);
	private float _shipScale = 1; //ship width = 200
	private float _mapScale = 1;
	private int _mapWidth = 5;
	private GameUI _gameUI;
	private Map _map;
	private int _mapHeight = 10;

	private bool _placeShips = false;
	public enum ShipType{
		AircraftCarrier,
		AmphibiousAssault,
		Destroyer,
		Cruiser,
		Corvette,
		Speedboat,
		TestShip
	}

	[Export] public PackedScene[] ships;
	
	public override void _Ready()
	{
		_gameUI = GetNode<GameUI>("../GameUI");
		_map = GetNode<Map>("../Map");
		_creationPosition = _gameUI.GetSpawnPosition();

		AddShipStorage(ShipType.Speedboat); // For testing
		AddShipStorage(ShipType.Corvette);
		//AddShipStorage(ShipType.Destroyer);
		//AddShipStorage(ShipType.Cruiser);

		StageBeginn();
	}
	public override void _Process(double delta)
	{
		if(_placeShips)
		{
			if(_shipList.Count == 0)
			{
				CreateShip(_shipStorage[0]);
				_shipStorage.Remove(_shipStorage[0]);
			}
			else
			{
				if(_shipList[^1].IsPlaced())
				{
					CreateShip(_shipStorage[0]);
					_shipStorage.Remove(_shipStorage[0]);
				}
			}
			if(_shipStorage.Count == 0) { 
				_placeShips = false; 
				//call GetCords fpr list of Vector2I cords
				//send cords to board manager
			}
		}
	}
	public void AddShipStorage(ShipType newShip) { _shipStorage.Add(newShip); }

	private void StageBeginn(){
		CreateShipGrid();
		StartShipPlacement();
	}
	private void BoardPlacementFinished() { LockShips(); }
	private void StageEnd(){
		StoreShips();
		ClearBoard();
	}
	private void EnemyTurn(){foreach(ShipManager X in _shipList){X.HideShip();}}
	private void PlayerTurn(){foreach(ShipManager X in _shipList){X.UnhideShip();}}
	private void StartShipPlacement() { _placeShips = true; }
	public void ScaleShips(float scale){
		_shipScale = scale;
		foreach (ShipManager x in _shipList){x.ScaleShip(_shipScale);}
	}
	private void StoreShips(){foreach(ShipManager X in _shipList){AddShipStorage(X.GetShipType());}}
	private void ClearBoard(){foreach(ShipManager X in _shipList){X.DestroyShip();}}
	private void LockShips(){foreach(ShipManager X in _shipList){X.LockingShip();}}
	private void UnlockShips(){foreach(ShipManager X in _shipList){X.UnlockingShip();}}
	private void CreateShip(ShipType type){
		switch (type)
		{
			case ShipType.Speedboat:
				_shipList.Add((ShipManager)ships[0].Instantiate());
				break;
			case ShipType.Corvette:
				_shipList.Add((ShipManager)ships[1].Instantiate());
				break;
			case ShipType.Destroyer:
				_shipList.Add((ShipManager)ships[2].Instantiate());
				break;
			case ShipType.Cruiser:
				_shipList.Add((ShipManager)ships[3].Instantiate());
				break;
			case ShipType.AmphibiousAssault:
				_shipList.Add((ShipManager)ships[4].Instantiate());
				break;
			case ShipType.AircraftCarrier:
				_shipList.Add((ShipManager)ships[5].Instantiate());
				break;
			case ShipType.TestShip:
				_shipList.Add((ShipManager)ships[6].Instantiate());
				break;
			default:
				GD.PrintErr("CreateShip Error");
				break;
		}
		AddChild(_shipList[^1]);
		_shipList[^1].Position = _creationPosition;
		_shipList[^1].SetShipType(type);
		_shipList[^1].ScaleShip(_shipScale);
		_shipList[^1].SetMapScale(_mapScale);
	}
	private void CreateShipGrid(){
		//int[,] map = GetMaop();   
			  
		_mapGridPosition = _gameUI.GetGameFieldPosition();
		_mapSize = _gameUI.GetGameFieldSize();
		_mapWidth = _map.GetMapWidth();

		int [,] board = _map.GetMap();

		SetShipScaling();
		PlaceShipGrid();
		GenerateShipGrid(board);
	}
	private void SetShipScaling(){
		_shipScale = (_mapSize.X / _mapWidth) / 200; // 200 is default size of the sprites
	}
	private void GenerateShipGrid(int[,] map){
		for (int i = 0; i < _mapWidth; i++)
		{
			for (int j = 0; j < _mapWidth; j++)
			{
				Vector2I placmentVector = new Vector2I(j,i);
				if(map[i,j] == 0){PlaceWater(placmentVector);}
				if(map[i,j] == 1){PlaceLand(placmentVector);}
			}
		}
	}
	private void PlaceShipGrid(){
		GetNode<TileMapLayer>("ShipGrid").Position = _mapGridPosition;
		_mapScale = (_mapSize.X / _mapWidth) / 16; // 16 is the map tile size
		GetNode<TileMapLayer>("ShipGrid").Scale = new Vector2 (_mapScale,_mapScale);
	}
	private void PlaceLand(Vector2I position) { GetNode<TileMapLayer>("ShipGrid").SetCell(position,4,new Vector2I(0,0),0); }
	private void PlaceWater(Vector2I position){ GetNode<TileMapLayer>("ShipGrid").SetCell(position,5,new Vector2I(0,0),0); }
}
