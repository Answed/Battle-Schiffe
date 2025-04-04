namespace BattleSchiffe.Scripts.MapGen;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MapGen : Node
{
	public enum IslandType
	{
		Chunker, // 0
		//Smallboy // 1
	}

	public struct IslandInfo
	{
		public Vector2I coordinates;
		public IslandType type;
	}
	
	[Signal] public delegate void MapGenerationFinishedEventHandler();
	[Signal] public delegate void GivingIslandsEventHandler();
	
	private int _mapWidth;
	private int _mapHeight;
	private int[,] _mapGrid;
	
	public List<IslandInfo> islandData = new(); //Needs to be public to be used by the inscpector

	private List<int[,]> _presets = new List<int[,]> //currently for testing
	{
		//new int[,] {{1, 1, 1}, {1, 0, 1}, {1, 1, 1}},
		//new int[,] {{1, 1}, {1, 1}}
		new int [,] {{1,1,1},{1,1,1},{1,1,1}} // 0
	};

	private bool CanPlacePreset(int[,] preset, int x, int y)
	{
		int presetHeight = preset.GetLength(0);
		int presetWidth = preset.GetLength(1);

		if (x + presetWidth > _mapWidth || y + presetHeight > _mapHeight)
			return false;

		for (int i = 0; i < presetHeight; i++)
		{
			for (int j = 0; j < presetWidth; j++)
			{
				if (preset[i, j] == 1 && _mapGrid[y + i, x + j] == 1)
					return false;
			}
		}
		return true;
	}

	private void PlacePreset(int[,] preset, int x, int y)
	{
		int presetHeight = preset.GetLength(0);
		int presetWidth = preset.GetLength(1);

		for (int i = 0; i < presetHeight; i++)
		{
			for (int j = 0; j < presetWidth; j++)
			{
				if (preset[i, j] == 1)
					_mapGrid[y + i, x + j] = 1;
			}
		}
	}

	private double CalculateCoverage()
	{
		int filledCells = 0;
		foreach (int cell in _mapGrid)
		{
			if (cell == 1)
				filledCells++;
		}
		int totalCells = _mapWidth * _mapHeight;
		return (filledCells / (double)totalCells) * 100;
	}

	public int[,] GetMapGrid() { return _mapGrid; }

	private void GenerateMap(int width, int height)
	{
		islandData = new();
		_mapWidth = width;
		_mapHeight = height;
		_mapGrid = new int[_mapWidth, _mapHeight];

		Random random = new Random();
		double targetCoverage = 30.0;

		while (CalculateCoverage() < targetCoverage)
		{
			int choosenIsland = random.Next(_presets.Count);
			int[,] preset = _presets[choosenIsland];
			int x = random.Next(0, _mapWidth);
			int y = random.Next(0, _mapHeight);
			

			if (CanPlacePreset(preset, x, y))
			{
				PlacePreset(preset, x, y);
				//safe which preset and preset psotion
				//all preset need to be alligined to their top left corner with scene centerS

				SaveIsland(choosenIsland, x,y);
			}
		}
		EmitSignal("MapGenerationFinished"); //struct is to complex for emitsignal
		EmitSignal("GivingIslands");
	}

	private void SaveIsland(int island, int x, int y){
		IslandInfo currentAddIsland = new IslandInfo();
		currentAddIsland.coordinates = new Vector2I(x,y);

		switch (island)
		{
			case 0:
				currentAddIsland.type = 0;
				break;
			case 1:
				//islandInfo.Type = IslandType.Smallboy;
				break;
			default:
				break;
		}
		islandData.Add(currentAddIsland);
	}
	public int GetMapWidth(){return _mapWidth; }

	public List<IslandInfo> GetIslandInfos() { return islandData; }
}
