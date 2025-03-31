
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
	
	private GameUI gameUI;
	private int MapWidth = 10;
	private void CallGenerateMap(int width, int height)
	{
		EmitSignal(SignalName.GenerateMap,width,height);
		MapWidth = width;
		EmitSignal(SignalName.SendMapWidth, width);
	}

	private void GetUIData()
	{
		gameUI = GetNode<GameUI>("../GameUI");
		EmitSignal("SendPosition",gameUI.getGameFieldPosition());
		EmitSignal("SendSize",gameUI.getGameFieldSize());
	}
	public int getMapWidth() { return MapWidth; }
	public void GetIslandInfos() { GetNode<MapUI>("MapUI").setIslandInfos(GetNode<MapGen>("MapGen").GetIslandInfos());}
	public int[,] GetMap() { return GetNode<MapGen>("MapGen").GetMapGrid(); }
	
	public void HideMap() { GetNode<Node2D>("MapUI").Visible = false; }
	
	public void ShowMap() { GetNode<Node2D>("MapUI").Visible = true; }
}
