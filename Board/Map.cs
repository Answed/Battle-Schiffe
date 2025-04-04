
using BattleSchiffe.Scripts.MapGen;
using Godot;
using System;
using System.Collections.Generic;

public partial class Map : Node
{
	[Signal] public delegate void GenerateMapEventHandler(int width, int height);
	[Signal] public delegate void SendPositionEventHandler(Vector2 position);
	[Signal] public delegate void SendSizeEventHandler(Vector2 size);
	[Signal] public delegate void SendMapWidthEventHandler(int width);
	
	[Signal] public delegate void SetHitMarkerEventHandler(string grid, Vector2I position);
	[Signal] public delegate void SetMissMarkerEventHandler(string grid, Vector2I position);
	
	private GameUI _gameUI;
	private int _mapWidth = 10; // map is currently only a square so only the width is needed
	
	private void CallGenerateMap(int width, int height)
	{
		EmitSignal(SignalName.GenerateMap,width, height);
		_mapWidth = width;
		EmitSignal(SignalName.SendMapWidth, width);
	}

	private void SetUIData()
	{
		_gameUI = GetNode<GameUI>("../GameUI");
		EmitSignal("SendPosition",_gameUI.GetGameFieldPosition());
		EmitSignal("SendSize",_gameUI.GetGameFieldSize());
	}
	public int GetMapWidth() { return _mapWidth; }
	private void GetIslandInfos() { GetNode<MapUI>("MapUI").SetIslandInfos(GetNode<MapGen>("MapGen").GetIslandInfos());}
	public int[,] GetMap() { return GetNode<MapGen>("MapGen").GetMapGrid(); }
	private void HideMap() { GetNode<Node2D>("MapUI").Visible = false; }
	private void ShowMap() { GetNode<Node2D>("MapUI").Visible = true; }
	private void TransferHitSignal(string grid, Vector2I position) { EmitSignal("SetHitMarker", grid, position); }
	private void TransferMissSignal(string grid, Vector2I position) { EmitSignal("SetMissMarker", grid, position); }
}
