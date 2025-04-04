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
	[Signal] public delegate void GameOverEventHandler(bool win);
	
	private int[,] _shipsPositions;
	protected List<Ship> Ships = new List<Ship>();
	private string _gridName;
	private bool _isPlayerBoard;
	
	private GameManager _gameManager;

	public override void _Ready() // Must be public to be used by the Engine
	{ 
		_gameManager = GetNode<GameManager>("../GameManager");
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
			//Gets the ShipValue from the Bord and substracts -2 to get the fitting index from Ships
			Ships[_shipsPositions[x, y] - 2].shipPosition.Remove(position);
			if (Ships[_shipsPositions[x, y] - 2].shipPosition.Count == 0)
			{
				Ships[_shipsPositions[position[0, 0], position[0, 1]] - 2].shipManager.DestroyShip();
				Ships.RemoveAt(_shipsPositions[x, y] - 2); 
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
		if (Ships.Count == 0)
			EmitSignal("GameOver", _isPlayerBoard);
	}
	protected void SetShipArray()
	{
		int currentShipIndex = 2;
		foreach (Ship ship in Ships)
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
	public List<Ship> GetShips() { return Ships; }
	public int[,] GetBoard() { return _shipsPositions; }
}
