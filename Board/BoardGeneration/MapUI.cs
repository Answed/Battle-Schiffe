using BattleSchiffe.Scripts.MapGen;
using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class MapUI : Node2D
{
	[Signal] public delegate void GetUIDataEventHandler();
	[Export] public CompressedTexture2D[] IslandSprites;// = new CompressedTexture2D();
	MapGen mapGen;
	TileMapLayer waterLayer;
	List<Sprite2D> Islands = new();
	private float scale = 1F;
	private float boardWidth = 418F;
	private int mapWidth = 10;
	private Vector2 mapPositon;

	private bool pReady = false;
	private bool sReady = false;
	public override void _Ready()
	{
		EmitSignal("GetUIData");
		waterLayer = GetNode<TileMapLayer>("Water");
	}


	public override void _Process(double delta)
	{
		if(pReady && sReady)
		{
			scale = boardWidth / ( 200F * mapWidth );
			waterLayer.Scale = new Vector2(scale,scale);
			waterLayer.Position = mapPositon;
			generateUI();
			//reset
			pReady = false;
			sReady = false;
		}
	}
	public void generateUI(){
		//fills board with water
		for (int i = 0; i < mapWidth; i++)
		{
			for (int j = 0; j < mapWidth; j++)
			{
				Vector2I position = new Vector2I(i,j);
				GetNode<TileMapLayer>("Water").SetCell(position,0,new Vector2I(0,0),0);
			}
		}
		//adds the islands
		placeIslands();
	}

	private void placeIslands(){
		Islands.Add(new Sprite2D());
		Islands[0].Texture = IslandSprites[0];
		foreach (Sprite2D X in Islands) 
		{ 
			X.Scale = new Vector2(scale,scale);
			AddChild(X);
		}
	}

	private void GetPosition(Vector2 positon) 
	{ 
		mapPositon = positon; 
		pReady = true;
	}
	private void GetSize(Vector2 size) 
	{ 
		boardWidth = size.X; 
		sReady = true;
	}
}
