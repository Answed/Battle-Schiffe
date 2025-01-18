namespace BattleSchiffe.Scripts.BoardManager;
using Godot;
using System;
using System.Collections.Generic;

public struct Ship
{
	//public ShipManager shipManager;  TODO add ShipManger
	public List<int[,]> shipPosition;
}

// 0 Water, 1 Land, >=2 is ship in list

public partial class BoardManager : Node
{
	private int[,] shipsPositions;
	protected List<Ship> ships = new List<Ship>();
	private Node boardNode;
	private bool isPlayerBoard;

	public void InitBoard(int[,] board, bool isPlayerBoard, Node boardNode) 
	{
		this.boardNode = boardNode;
		shipsPositions = board;
		this.isPlayerBoard = isPlayerBoard;
	}

	public void AttackField(int x, int y)
	{
		int[,] position = {{x},{y}};
		if (shipsPositions[position[0, 0], position[0,1]] >= 2)
		{
			ships[shipsPositions[position[0, 0], position[0,1]] - 2].shipPosition.Remove(position);
			if (ships[shipsPositions[position[0, 0], position[0, 1]] - 2].shipPosition.Count == 0)
			{
				//ships[shipsPositions[position[0, 0], position[0, 1]] - 2].shipManager.DestroyShip();
				ships.RemoveAt(shipsPositions[position[0, 0], position[0,1]] - 2); // -2 because or ship index on the map starts with 2 and the list with 0. So the first ship in the list is the ship 2 on the maps
				CheckIfAllShipsAreDead();
			}
			//Set X on position
		}
		else
		{
			// Set 0 on position
		}
	}

	private void CheckIfAllShipsAreDead()
	{
		if (ships.Count == 0)
		{
			//Call GameManger -> GameLost transfer bool value isPlayerBoard to check in the game manager who has won
		}
	}

	public void DeleteBoard()
	{
		boardNode.QueueFree();
	}

	public void SetShipArray()
	{
		int currentShipIndex = 2;
		foreach (Ship ship in ships)
		{
			foreach (int[,] position in ship.shipPosition)
			{
				shipsPositions[position[0, 0], position[0,1]] = currentShipIndex;
			}
			currentShipIndex++;
		}
	}

	public List<Ship> GetShips()
	{
		return ships;
	}
}

