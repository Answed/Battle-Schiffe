namespace BattleSchiffe.Scripts.MapGen;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public enum IslandType
{
	Chunker, // 0
	//Smallboy // 1
}

public struct IslandInfo
{
	public Vector2I cordinate;
	public int Type;
}

public partial class MapGen : Node
{
	public enum IslandType
	{
		Chunker, // 0
		//Smallboy // 1
	}

	public struct IslandInfo
	{
		public Vector2I cordinate;
		public IslandType Type;
	}
	[Signal] public delegate void MapGenerationFinishedEventHandler();
	[Signal] public delegate void GivingIslandsEventHandler();
	private int mapWidth;
	private int mapHeight;
	private int[,] mapGrid;

	public List<IslandInfo> islandData = new();

	private List<int[,]> presets = new List<int[,]>
	{
		//new int[,] {{1, 1, 1}, {1, 0, 1}, {1, 1, 1}},
		//new int[,] {{1, 1}, {1, 1}}
		new int [,] {{1,1,1},{1,1,1},{1,1,1}} // 0
	};

	private bool CanPlacePreset(int[,] preset, int x, int y)
	{
		int presetHeight = preset.GetLength(0);
		int presetWidth = preset.GetLength(1);

		if (x + presetWidth > mapWidth || y + presetHeight > mapHeight)
			return false;

		for (int i = 0; i < presetHeight; i++)
		{
			for (int j = 0; j < presetWidth; j++)
			{
				if (preset[i, j] == 1 && mapGrid[y + i, x + j] == 1)
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
					mapGrid[y + i, x + j] = 1;
			}
		}
	}

	private double CalculateCoverage()
	{
		int filledCells = 0;
		foreach (int cell in mapGrid)
		{
			if (cell == 1)
				filledCells++;
		}
		int totalCells = mapWidth * mapHeight;
		return (filledCells / (double)totalCells) * 100;
	}

	public int[,] GetMapGrid()
	{
		return mapGrid;
	}

	public void GenerateMap(int width, int height)
	{
		islandData = new();
		mapWidth = width;
		mapHeight = height;
		mapGrid = new int[mapWidth, mapHeight];

		Random random = new Random();
		double targetCoverage = 30.0;

		while (CalculateCoverage() < targetCoverage)
		{
			int choosenIsland = random.Next(presets.Count);
			int[,] preset = presets[choosenIsland];
			int x = random.Next(0, mapWidth);
			int y = random.Next(0, mapHeight);
			

			if (CanPlacePreset(preset, x, y))
			{
				PlacePreset(preset, x, y);
				//safe which preset and preset psotion
				//all preset need to be alligined to their top left corner with scene center
				saveIsland(choosenIsland, x,y);
			}
		}
		//debug prints map
		/*for (int i = 0; i < mapGrid.GetLength(0); i++)
		{
			string line = "";
			for (int j = 0; j < mapGrid.GetLength(1); j++)
			{
				line += mapGrid[i, j] + " ";
			}
			GD.Print(line);
		}*/
		EmitSignal("MapGenerationFinished"); //struct is to complex for emitsignal
		EmitSignal("GivingIslands");
	}

	private void saveIsland(int island, int x, int y){
		IslandInfo currentAddIsland = new IslandInfo();
		currentAddIsland.cordinate = new Vector2I(x,y);

		switch (island)
		{
			case 0:
				currentAddIsland.Type = 0;
				break;
			case 1:
				//islandInfo.Type = IslandType.Smallboy;
				break;
			default:
				break;
		}
		islandData.Add(currentAddIsland);
	}
	public int getMapWidth(){return mapWidth; }

	public List<IslandInfo> GetIslandInfos() { return islandData; }
}
