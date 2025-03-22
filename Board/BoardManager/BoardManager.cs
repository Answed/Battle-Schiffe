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
	private int [,] oldBoardPositions;
	protected List<Ship> ships = new List<Ship>();
	private Node boardNode;
	private bool isPlayerBoard;
	
	private GameManager gameManager;

	public override void _Ready()
	{
		//gameManager = GetNode<GameManager>("../GameManager");
	}
	
	public void InitBoard(int[,] board, bool isPlayerBoard, Node boardNode) 
	{
		this.boardNode = boardNode;
		shipsPositions = board;
		this.isPlayerBoard = isPlayerBoard;
	}

	public void AttackField(int x, int y)
	{
		int[,] position = {{x},{y}};
		/*if (shipsPositions[x, y] >= 2)
		{
			//Gets the ShipValue from the Bord and substracts -2 to get the fitting index from ships
			//ships[shipsPositions[x, y] - 2].shipPosition.Remove(position);
			if (ships[shipsPositions[x, y] - 2].shipPosition.Count == 0)
			{
				//ships[shipsPositions[position[0, 0], position[0, 1]] - 2].shipManager.DestroyShip();
				ships.RemoveAt(shipsPositions[x, y] - 2); 
				//CheckIfAllShipsAreDead();
			}
			//Set X on position
		}
		else
		{
			// Set 0 on position
		}*/
		shipsPositions[x, y] = -1; // Marks all hittet fields negativ Currently just for testing
	}

	private void CheckIfAllShipsAreDead()
	{
		if (ships.Count == 0)
			gameManager.PlayerHasWon(isPlayerBoard);
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

	public int[,] GetBoard()
	{
		return shipsPositions;
	}
}
