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
	TileMapLayer gridLayer;
	List<Sprite2D> Islands = new();
	private float scale = 1F;
	private float boardWidth = 418F;
	private int mapWidth = 10;
	private Vector2 mapPositon;

	private List<MapGen.IslandInfo> IslandInfo;
	private bool pReady = false;
	private bool sReady = false;
	private bool mReady = false;
	public override void _Ready()
	{
		EmitSignal("GetUIData");
		waterLayer = GetNode<TileMapLayer>("Water");
		gridLayer = GetNode<TileMapLayer>("Grid");

	}


	public override void _Process(double delta)
	{
		if(pReady && sReady && mReady)
		{
			scale = boardWidth / ( 200F * mapWidth );
			waterLayer.Scale = new Vector2(scale,scale);
			waterLayer.Position = mapPositon;
			gridLayer.Scale = new Vector2(scale,scale);
			gridLayer.Position = mapPositon;

			generateUI();
			//reset
			pReady = false;
			sReady = false;
			mReady = false;
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
		for (int i = 0; i < mapWidth; i++)
		{
			for (int j = 0; j < mapWidth; j++)
			{
				Vector2I position = new Vector2I(i,j);
				GetNode<TileMapLayer>("Grid").SetCell(position,0,new Vector2I(0,0),0);
			}
		}
		//adds the islands
		placeIslands();
	}

	private void placeIslands(){
		foreach (MapGen.IslandInfo X in IslandInfo)
		{
			Islands.Add(new Sprite2D());
			switch (X.Type)
			{
				case MapGen.IslandType.Chunker:
					Islands[^1].Texture = IslandSprites[0];
					break;
				default:
					break;
			}
			Islands[^1].Scale = new Vector2(scale,scale);
			Islands[^1].Position = waterLayer.MapToLocal(X.cordinate);
			Islands[^1].Position = new Vector2 (Islands[^1].Position.X * scale, Islands[^1].Position.Y * scale);
			Islands[^1].Position = new Vector2 (Islands[^1].Position.X + mapPositon.X, Islands[^1].Position.Y + mapPositon.Y);
			Islands[^1].Position = new Vector2 (Islands[^1].Position.X + (200 * scale),Islands[^1].Position.Y + (200 * scale));
		}
		foreach (Sprite2D X in Islands) { AddChild(X); }
	}
	public void setIslandInfos(List<MapGen.IslandInfo> Infos) { 
		mReady = true;
		IslandInfo = Infos; 
	}
	private void MapReady(){
		//mReady = true;
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
