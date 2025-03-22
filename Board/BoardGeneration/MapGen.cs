namespace BattleSchiffe.Scripts.MapGen;
using Godot;
using System;
using System.Collections.Generic;

public partial class MapGen : Node
{
	private int mapWidth;
	private int mapHeight;
	private int[,] mapGrid;

	private List<int[,]> presets = new List<int[,]>
	{
		new int[,] {{1, 1, 1}, {1, 0, 1}, {1, 1, 1}},
		new int[,] {{1, 1}, {1, 1}}
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
		GD.PrintErr("Hallo");
		mapWidth = width;
		mapHeight = height;
		mapGrid = new int[mapWidth, mapHeight];

		Random random = new Random();
		double targetCoverage = 30.0;

		while (CalculateCoverage() < targetCoverage)
		{
			int[,] preset = presets[random.Next(presets.Count)];
			int x = random.Next(0, mapWidth);
			int y = random.Next(0, mapHeight);

			if (CanPlacePreset(preset, x, y))
			{
				PlacePreset(preset, x, y);
				//safe which preset and preset psotion
				//all preset need to be alligined to their top left corner with scene center
			}
		}
	}
}
