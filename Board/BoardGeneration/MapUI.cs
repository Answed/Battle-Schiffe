using BattleSchiffe.Scripts.MapGen;
using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class MapUI : Node2D
{
	[Signal] public delegate void GetUIDataEventHandler();
	[Export] public CompressedTexture2D[] islandSprites;// = new CompressedTexture2D();
	private TileMapLayer _waterLayer;
	private TileMapLayer _gridLayer;
	private List<Sprite2D> _islands = new();
	private float _scale = 1F;
	private float _boardWidth = 418F;
	private int _mapWidth = 10;
	private Vector2 _mapPositon;
	private List<MapGen.IslandInfo> _islandInfos;
	private bool _pReady = false;
	private bool _sReady = false;
	private bool _mReady = false;
	private bool _wReady = false;
	
	public override void _Ready()
	{
		EmitSignal("GetUIData");
		_waterLayer = GetNode<TileMapLayer>("Water");
		_gridLayer = GetNode<TileMapLayer>("Grid");
	}

	public override void _Process(double delta)
	{
		if(_pReady && _sReady && _mReady && _wReady)
		{
			_scale = _boardWidth / ( 200F * _mapWidth );
			_waterLayer.Scale = new Vector2(_scale,_scale);
			_waterLayer.Position = _mapPositon;
			_gridLayer.Scale = new Vector2(_scale,_scale);
			_gridLayer.Position = _mapPositon;
			GD.Print("Hello");
			GenerateUI();
			//reset
			_pReady = false;
			_sReady = false;
			_mReady = false;
			_wReady = false;
		}
	}
	private void GenerateUI()
	{
		// fills board with water
		GenerateTileMap("Water");
		// adds guidelines to make the grid visible for the player.
		GenerateTileMap("Grid");
		// adds the islands
		PlaceIslands();
		
		// Creates empty maps which will later be used to indicate hits
		GenerateTileMap("PlayerGrid");
		GenerateTileMap("EnemyGrid");
	}

	private void GenerateTileMap(string mapName)
	{
		for (int i = 0; i < _mapWidth; i++)
		{
			for (int j = 0; j < _mapWidth; j++)
			{
				Vector2I position = new Vector2I(i,j);
				GetNode<TileMapLayer>(mapName).SetCell(position,0,new Vector2I(0,0),0);
			}
		}
	}

	private void PlaceIslands()
	{
		foreach (MapGen.IslandInfo x in _islandInfos)
		{
			_islands.Add(new Sprite2D());
			switch (x.type)
			{
				case MapGen.IslandType.Chunker:
					_islands[^1].Texture = islandSprites[0];
					break;
				default:
					break;
			}
			_islands[^1].Scale = new Vector2(_scale,_scale);
			_islands[^1].Position = _waterLayer.MapToLocal(x.coordinates);
			_islands[^1].Position = new Vector2 (_islands[^1].Position.X * _scale, _islands[^1].Position.Y * _scale);
			_islands[^1].Position = new Vector2 (_islands[^1].Position.X + _mapPositon.X, _islands[^1].Position.Y + _mapPositon.Y);
			_islands[^1].Position = new Vector2 (_islands[^1].Position.X + (200 * _scale),_islands[^1].Position.Y + (200 * _scale));
		}
		foreach (Sprite2D X in _islands) { AddChild(X); }
	}
	public void SetIslandInfos(List<MapGen.IslandInfo> infos)
	{ 
		_mReady = true;
		_islandInfos = infos; 
	}
	private void MapReady(){
		//mReady = true;
	}
	private void SetMapPosition(Vector2 positon) //Node already has a function called SetPosition
	{ 
		_mapPositon = positon; 
		_pReady = true;
	}
	private void SetSize(Vector2 size) 
	{ 
		_boardWidth = size.X; 
		_sReady = true;
	}
	private void SetWidth(int width)
	{
		_mapWidth = width;
		_wReady = true;
	}
	private void PlaceHitMarker(string grid, Vector2I pos) { GetNode<TileMapLayer>(grid).SetCell(pos,1,new Vector2I(0,0),0); }
	private void PlaceMissMarker(string grid, Vector2I pos) { GetNode<TileMapLayer>(grid).SetCell(pos,2,new Vector2I(0,0),0); }
}
