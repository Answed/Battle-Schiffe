namespace BattleSchiffe.Scripts.BoardManager;
using Godot;
using System;
using System.Collections.Generic;

public struct Ship
{
	public ShipManager shipManager;  
	public List<int[,]> shipPosition;
}

// 0 Water, 1 Land, >=2 is ship in list

public partial class BoardManager : Node
{
	[Signal] public delegate void SetHitMarkerEventHandler(string grid, Vector2I position);
	[Signal] public delegate void SetMissMarkerEventHandler(string grid, Vector2I position);
	
	private int[,] _shipsPositions;
	private int [,] _oldBoardPositions;
	protected List<Ship> ships = new List<Ship>();
	private string _gridName;
	private bool _isPlayerBoard;
	
	private GameManager gameManager;

	public override void _Ready()
	{ 
		gameManager = GetNode<GameManager>("../GameManager");
	}
	
	public void InitBoard(int[,] board, bool isPlayerBoard) 
	{
		_shipsPositions = CopyBoard(board);
		this._isPlayerBoard = isPlayerBoard;
		_gridName = isPlayerBoard ? "PlayerGrid" : "EnemyGrid";
	}

	public void AttackField(int x, int y)
	{
		int[,] position = {{x},{y}};
		if (_shipsPositions[x, y] >= 2) 
		{
			//Gets the ShipValue from the Bord and substracts -2 to get the fitting index from ships
			ships[_shipsPositions[x, y] - 2].shipPosition.Remove(position);
			if (ships[_shipsPositions[x, y] - 2].shipPosition.Count == 0)
			{
				ships[_shipsPositions[position[0, 0], position[0, 1]] - 2].shipManager.DestroyShip();
				ships.RemoveAt(_shipsPositions[x, y] - 2); 
				CheckIfAllShipsAreDead();
			}
			EmitSignal("SetHitMaker",_gridName, new Vector2I(x,y));
		}
		else
			EmitSignal("SetMissMarker",_gridName, new Vector2I(x,y));
		
		_shipsPositions[x, y] = -1; // Marks all hittet fields negativ Currently just for testing
	}

	private void CheckIfAllShipsAreDead()
	{
		if (ships.Count == 0)
			gameManager.PlayerHasWon(_isPlayerBoard);
	}
	public void SetShipArray()
	{
		int currentShipIndex = 2;
		foreach (Ship ship in ships)
		{
			foreach (int[,] position in ship.shipPosition)
			{
				_shipsPositions[position[0, 0], position[0,1]] = currentShipIndex;
			}
			currentShipIndex++;
		}
	}
	private int[,] CopyBoard(int[,] originalBoard) // It needs to be copied to prevent overwriting the other bord
	{
		int[,] newBoard = new int[originalBoard.GetLength(0), originalBoard.GetLength(1)];
		for (int i = 0; i < originalBoard.GetLength(0); i++)
		{
			for (int j = 0; j < originalBoard.GetLength(1); j++)
			{
				newBoard[i, j] = originalBoard[i, j];
			}
		}	
		return newBoard;
	}
	public List<Ship> GetShips() { return ships; }
	public int[,] GetBoard() { return _shipsPositions; }
}
