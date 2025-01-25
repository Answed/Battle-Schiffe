using System;
using System.Collections.Generic;

public class IslandGenerator
{
    static int mapWidth;
    static int mapHeight;
    static int[,] mapGrid;
    static List<int[,]> presets = new List<int[,]>
    {
        new int[,] {{1, 1, 1}, {1, 0, 1}, {1, 1, 1}},
        new int[,] {{1, 1}, {1, 1}}
    };

    static bool CanPlacePreset(int[,] preset, int x, int y)
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

    static void PlacePreset(int[,] preset, int x, int y)
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

    static double CalculateCoverage()
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

    static void PrintMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                Console.Write(mapGrid[y, x] + " ");
            }
            Console.WriteLine();
        }
    }

    public static int[,] GetMapGrid()
    {
        return mapGrid;
    }

    public static void Main()
    {
        Console.Write("Enter map width: ");
        mapWidth = int.Parse(Console.ReadLine());
        Console.Write("Enter map height: ");
        mapHeight = int.Parse(Console.ReadLine());

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
            }
        }

        Console.WriteLine("Map generation complete!");
        PrintMap();
    }
}